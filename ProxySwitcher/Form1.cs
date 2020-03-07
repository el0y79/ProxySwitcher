using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ProxySwitcher
{
    public partial class ProxySwitcherForm : Form
    {
        private ConfigurationLoader configurationLoader = new ConfigurationLoader();
        private bool exiting = false;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        public ProxySwitcherForm()
        {
            InitializeComponent();
            NetworkChange.NetworkAddressChanged += OnNetworkChanged;
        }

        private void OnNetworkChanged(object sender, EventArgs e)
        {
            try
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                List<ProxyConfig> configuration = configurationLoader.LoadConfiguration();
                foreach (NetworkInterface networkInterface in adapters)
                {
                    if (!networkInterface.Supports(NetworkInterfaceComponent.IPv4) || networkInterface.OperationalStatus != OperationalStatus.Up)
                    {
                        continue;
                    }

                    var properties = networkInterface.GetIPProperties();
                    foreach (var address in properties.UnicastAddresses)
                    {
                        var ip = address.Address;
                        var ipString = ip.ToString();
                        foreach (var config in configuration)
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
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            ConfigurationLoader loader = new ConfigurationLoader();
            var cfg = loader.LoadConfiguration();
            foreach (var config in cfg)
            {
                var button = new ToolStripButton(config.Name);
                button.Click += Button_Click;
                contextMenuStrip1.Items.Add(button);
            }

            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            var exitButton = new ToolStripButton("Exit");
            exitButton.Click += ExitButton_Click;
            contextMenuStrip1.Items.Add(exitButton);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            exiting = true;
            Close();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var cfg = configurationLoader.LoadConfiguration();
            var match = cfg.FirstOrDefault(x => ((ToolStripButton)sender).Text.Equals(x.Name));
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
            Process process = Process.Start("netsh", cmd);
            process.WaitForExit();

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
                return;
            }
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
        }
    }
}
