using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using ProxySwitcher.Properties;
using Timer = System.Timers.Timer;

namespace ProxySwitcher
{
    public partial class ProxySwitcherForm : Form
    {
        private ConfigurationLoader configurationLoader = new ConfigurationLoader();
        private Configuration configuration;
        private bool exiting = false;
        private int retries = 10;
        private TimeSpan delay = TimeSpan.FromSeconds(1);
        private event Action<ProxyConfig> OnConfigChanged;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;

        public ProxySwitcherForm()
        {
            InitializeComponent();
            configuration = configurationLoader.LoadConfiguration();
            NetworkChange.NetworkAddressChanged += OnNetworkChangedEventHandler;
            OnConfigChanged += ProxySwitcherForm_OnConfigChanged;
        }

        private void ProxySwitcherForm_OnConfigChanged(ProxyConfig newConfig)
        {
            var newLabelText = newConfig?.Name ?? "None";
            if (newLabelText.Equals(lblCurrentConfig.Text))
            {
                return;
            }

            lblCurrentConfig.Text = newLabelText;
            trayIcon.Text = "Current Config: " + lblCurrentConfig.Text;
            trayIcon.BalloonTipTitle = "Network Change";
            trayIcon.BalloonTipText = $"New Proxy Config: {newLabelText}";
            trayIcon.ShowBalloonTip(2000);
        }

        private void OnNetworkChangedEventHandler(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < retries; i++)
                {
                    OnNetworkChanged();
                    Thread.Sleep(delay);
                }
            });
        }

        private string GetLocalEndPoint(string proxy)
        {
            IPHostEntry entry = null;
            try
            {
                Uri uri = null;
                if (!Uri.TryCreate("http://" + proxy, UriKind.Absolute, out uri))
                {
                    return null;
                }

                entry = Dns.GetHostEntry(uri.Host);
            }
            catch (Exception exc)
            {
                entry = null;
            }

            if (entry == null || !entry.AddressList.Any())
            {
                return null;
            }

            try
            {
                IPAddress remoteIp = entry.AddressList.First().MapToIPv4();
                IPEndPoint remoteEndPoint = new IPEndPoint(remoteIp, 0);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                SocketAddress address = remoteEndPoint.Serialize();

                byte[] remoteAddrBytes = new byte[address.Size];
                for (int i = 0; i < address.Size; i++)
                {
                    remoteAddrBytes[i] = address[i];
                }

                byte[] outBytes = new byte[remoteAddrBytes.Length];
                socket.IOControl(
                    IOControlCode.RoutingInterfaceQuery,
                    remoteAddrBytes,
                    outBytes);
                for (int i = 0; i < address.Size; i++)
                {
                    address[i] = outBytes[i];
                }

                EndPoint ep = remoteEndPoint.Create(address);
                return ((IPEndPoint)ep).Address.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void OnNetworkChanged()
        {
            if (!configuration.AutoUpdate)
            {
                return;
            }
            try
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in adapters)
                {
                    if (!networkInterface.Supports(NetworkInterfaceComponent.IPv4)
                        || networkInterface.OperationalStatus != OperationalStatus.Up
                        || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                    {
                        continue;
                    }

                    var properties = networkInterface.GetIPProperties();
                    foreach (var address in properties.UnicastAddresses)
                    {
                        var ip = address.Address;
                        var ipString = ip.ToString();
                        foreach (var config in configuration.Proxies.OrderByDescending(x=>x.Proxy))
                        {
                            if (Regex.IsMatch(ipString, config.OwnIP))
                            {
                                if (!string.IsNullOrEmpty(config.Proxy))
                                {
                                    string localEndPoint = GetLocalEndPoint(config.Proxy);
                                    if (string.IsNullOrEmpty(localEndPoint))
                                    {
                                        continue; //ip matches but cannot be reached by that interface
                                    }
                                    if (!Regex.IsMatch(localEndPoint, config.OwnIP))
                                    {
                                        continue; //wrong interface
                                    }
                                }
                                ApplyProxy(config);
                                return;
                            }
                        }
                    }
                }
                ApplyProxy(null);
            }
            catch (Exception exc)
            {
                //noop
                ApplyProxy(null);
            }
        }


        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exiting = true;
            Close();
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            DlgSettings settings = new DlgSettings();
            settings.ShowDialog();
            configuration = settings.Configuration;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            foreach (var config in configuration.Proxies)
            {
                var button = new ToolStripMenuItem(config.Name);
                button.Image = Resources.entry;
                button.Click += Button_Click;
                contextMenuStrip1.Items.Add(button);
            }

            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            var exitButton = new ToolStripMenuItem("Exit");
            exitButton.Click += ExitButton_Click;
            exitButton.Image = Resources.exit;
            var settingsButton = new ToolStripMenuItem("Settings");
            settingsButton.Click += mnuSettings_Click;
            settingsButton.Image = Resources.settings;
            contextMenuStrip1.Items.Add(settingsButton);
            contextMenuStrip1.Items.Add(exitButton);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            exiting = true;
            Close();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var match = configuration.GetByName(((ToolStripItem)sender).Text);
            ApplyProxy(match);
        }

        private void ApplyProxy(ProxyConfig matchProxy)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            string cmd;
            if (string.IsNullOrEmpty(matchProxy?.Proxy.Trim()))
            {
                cmd = "winhttp reset proxy";
                registry.SetValue("ProxyEnable", 0);
                registry.SetValue("ProxyServer", "");
            }
            else
            {
                cmd = $"winhttp set proxy {matchProxy.Proxy}";
                registry.SetValue("ProxyEnable", 1);
                registry.SetValue("ProxyServer", matchProxy.Proxy);
            }

            if (configuration.ConsiderWinHTTP)
            {
                Process process = Process.Start("netsh", cmd);
                process.WaitForExit();
            }

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            Invoke(new Action(() =>
            {
                FireOnConfigChanged(matchProxy);
            }));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnNetworkChanged();
        }

        private void ProxySwitcherForm_Load(object sender, EventArgs e)
        {
            OnNetworkChanged();
        }

        private void ProxySwitcherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exiting)
            {
                trayIcon.Visible = false;
                return;
            }
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
            this.Visible = false;
        }

        private void FireOnConfigChanged(ProxyConfig newConfig)
        {
            OnConfigChanged?.Invoke(newConfig);
        }

    }
}
