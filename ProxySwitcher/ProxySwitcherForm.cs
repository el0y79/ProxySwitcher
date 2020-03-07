using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using ProxySwitcher.Properties;

namespace ProxySwitcher
{
    public partial class ProxySwitcherForm : Form
    {
        private ConfigurationLoader configurationLoader = new ConfigurationLoader();
        private Configuration configuration;
        private bool exiting = false;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;

        public ProxySwitcherForm()
        {
            InitializeComponent();
            configuration = configurationLoader.LoadConfiguration();
            NetworkChange.NetworkAddressChanged += OnNetworkChanged;
        }

        private void OnNetworkChanged(object sender, EventArgs e)
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
                        || networkInterface.OperationalStatus != OperationalStatus.Up)
                    {
                        continue;
                    }

                    var properties = networkInterface.GetIPProperties();
                    foreach (var address in properties.UnicastAddresses)
                    {
                        var ip = address.Address;
                        var ipString = ip.ToString();
                        foreach (var config in configuration.Proxies)
                        {
                            if (Regex.IsMatch(ipString, config.OwnIP))
                            {
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
            WindowState = FormWindowState.Normal;
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
            var match = configuration.GetByName(((ToolStripButton)sender).Text);
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
            Invoke(new Action(() => lblCurrentConfig.Text = matchProxy?.Name ?? "None"));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnNetworkChanged(null, null);
        }

        private void ProxySwitcherForm_Load(object sender, EventArgs e)
        {
            OnNetworkChanged(null, null);
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
        }
    }
}
