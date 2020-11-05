using System;
using System.Collections.Generic;
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

        private ProxyConfig currentProxyConfig = null;

        private Configuration Configuration
        {
            get => configuration;
            set
            {
                configuration = value;
                //we need to evaluate the cyclic check flag. if its set
                //we need to setup the timer. If it doesnt we need to stop the timer
                cyclicCheckTimer.Enabled = configuration.CyclicCheck;
            }
        }

        private bool exiting = false;
        private TimeSpan delay = TimeSpan.FromSeconds(1);
        private TimeSpan cyclícCheckInterval = TimeSpan.FromSeconds(10);
        private object threadLock = new object();
        private event Action<ProxyConfig> OnConfigChanged;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        private const string REG_KEY_PROXYENABLE = "ProxyEnable";
        private const string REG_KEY_PROXYSERVER = "ProxyServer";
        private const string REG_KEY_PROXYOVERRIDE = "ProxyOverride";
        private const string REG_VAL_PROXYOVERRIDE_LOCAL = "<local>";

        public ProxySwitcherForm()
        {
            InitializeComponent();
            cyclicCheckTimer.Interval = (int)cyclícCheckInterval.TotalMilliseconds;
            Configuration = configurationLoader.LoadConfiguration();
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
                for (int i = 0; i < (Configuration.CyclicCheck ? 1 : Configuration.RetryTimeSec); i++)
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
            catch (Exception)
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
            catch (Exception)
            {
                return null;
            }
        }

        private void OnNetworkChanged()
        {
            if (!Configuration.AutoUpdate)
            {
                return;
            }

            lock (threadLock)
            {
                try
                {
                    NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface networkInterface in adapters)
                    {
                        if (!networkInterface.Supports(NetworkInterfaceComponent.IPv4)
                            || networkInterface.OperationalStatus != OperationalStatus.Up
                            || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback
                            || !networkInterface.GetIPProperties().GatewayAddresses.Any())
                        {

                            continue;
                        }

                        var properties = networkInterface.GetIPProperties();
                        foreach (var address in properties.UnicastAddresses)
                        {
                            var ip = address.Address;
                            var ipString = ip.ToString();
                            foreach (var config in Configuration.Proxies.OrderByDescending(x => x.Proxy))
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
                catch (Exception)
                {
                    //noop
                    ApplyProxy(null);
                }
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
            DlgSettings settings = new DlgSettings(Configuration, ()=>currentProxyConfig);
            settings.ShowDialog();
            Configuration = settings.Configuration;
            configurationLoader.SaveConfiguration(Configuration);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            foreach (var config in Configuration.Proxies)
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
            var match = Configuration.GetByName(((ToolStripItem)sender).Text);
            ApplyProxy(match, true);
        }

        private void ApplyProxy(ProxyConfig matchProxy, bool forceUpdate = false)
        {
            if (forceUpdate || currentProxyConfig != matchProxy)
            {
                RegistryKey registry =
                    Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                        true);
                string bypassConfig = matchProxy.AdditionalExceptions;
                if (!string.IsNullOrWhiteSpace(bypassConfig) && !bypassConfig.EndsWith(";") && matchProxy.BypassLocal)
                {
                    bypassConfig += ";";
                }

                if (matchProxy.BypassLocal)
                {
                    bypassConfig += REG_VAL_PROXYOVERRIDE_LOCAL;
                }
                

                string winHttpCommand;
                List<string> gitCommands = new List<string>();
                if (string.IsNullOrEmpty(matchProxy?.Proxy.Trim()))
                {
                    winHttpCommand = "winhttp reset proxy";
                    gitCommands.Add("config --global --unset-all http.proxy");
                    registry.SetValue(REG_KEY_PROXYENABLE, 0);
                    registry.SetValue(REG_KEY_PROXYSERVER, "");
                    if(registry.GetValueNames().Contains(REG_KEY_PROXYOVERRIDE))
                        registry.DeleteValue(REG_KEY_PROXYOVERRIDE);
                }
                else
                {
                    winHttpCommand = $"winhttp set proxy {matchProxy.Proxy}";
                    
                    gitCommands.Add($"config --global --unset-all http.proxy");
                    gitCommands.Add($"config --global --add http.proxy http://{matchProxy.Proxy}");
                    registry.SetValue(REG_KEY_PROXYENABLE, 1);
                    registry.SetValue(REG_KEY_PROXYSERVER, matchProxy.Proxy);
                    if (!string.IsNullOrWhiteSpace(bypassConfig))
                    {
                        registry.SetValue(REG_KEY_PROXYOVERRIDE, bypassConfig);
                        winHttpCommand += $" \"{bypassConfig}\"";
                    }
                }

                if (Configuration.ConsiderWinHTTP)
                {
                    Process process = Process.Start("netsh", winHttpCommand);
                    process.WaitForExit();
                }

                if (Configuration.ConsiderGit && !string.IsNullOrEmpty(Configuration.PathToGitExecutable))
                {
                    gitCommands.ForEach(x =>
                    {
                        Process process = Process.Start(Configuration.PathToGitExecutable, x);
                        process.WaitForExit();
                    });

                }

                InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
                Invoke(new Action(() => { FireOnConfigChanged(matchProxy); }));
                currentProxyConfig = matchProxy;
            }
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
                cyclicCheckTimer.Enabled = false;
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

        private void cyclicCheckTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() => OnNetworkChanged());
        }
    }
}
