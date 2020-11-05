using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProxySwitcher
{
    public partial class DlgSettings : Form
    {
        public Configuration Configuration { get; private set; }
        private Func<ProxyConfig> currentProxyConfig;
        public DlgSettings(Configuration configuration, Func<ProxyConfig> currentProxyConfig)
        {
            InitializeComponent();
            Configuration = configuration;
            this.currentProxyConfig = currentProxyConfig;
            UpdateDialog();
        }

        private void UpdateDialog()
        {
            lstConfigurations.Items.Clear();
            Configuration.Proxies.ForEach(x => lstConfigurations.Items.Add(new ProxyConfigWrapper(x, currentProxyConfig)));
            chkAutoUpdate.Checked = Configuration.AutoUpdate;
            chkCyclicChecking.Checked = Configuration.CyclicCheck;
            chkConsiderWinHTTP.Checked = Configuration.ConsiderWinHTTP;
            txtRetrytimeAfterEvent.Text = Configuration.RetryTimeSec.ToString();
            chkGitSupport.Checked = Configuration.ConsiderGit;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("need a name");
                return;
            }
            if (string.IsNullOrEmpty(txtOwnIP.Text))
            {
                MessageBox.Show("need an IP");
                return;
            }

            var config = Configuration.Proxies.FirstOrDefault(x => x.Name.Equals(txtName.Text));
            if (config != null)
            {
                config.OwnIP = txtOwnIP.Text;
                config.Proxy = txtProxy.Text;
                config.BypassLocal = chkBypassLocal.Checked;
                config.AdditionalExceptions = txtAdditionalExceptions.Text;
            }
            else
            {
                config = new ProxyConfig
                {
                    Name = txtName.Text,
                    OwnIP = txtOwnIP.Text,
                    Proxy = txtProxy.Text,
                    BypassLocal = chkBypassLocal.Checked,
                    AdditionalExceptions = txtAdditionalExceptions.Text
                };
                Configuration.Proxies.Add(config);
                UpdateDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstConfigurations.SelectedItem == null)
            {
                return;
            }

            ProxyConfigWrapper config = (ProxyConfigWrapper)lstConfigurations.SelectedItem;
            Configuration.Proxies.Remove(config.Content);
            lstConfigurations.Items.Remove(config);
        }

        private void lstConfigurations_SelectedValueChanged(object sender, EventArgs e)
        {
            ProxyConfigWrapper config = (ProxyConfigWrapper)lstConfigurations.SelectedItem;
            if (config == null)
            {
                return;
            }
            txtName.Text = config.Content.Name;
            txtOwnIP.Text = config.Content.OwnIP;
            txtProxy.Text = config.Content.Proxy;
            chkBypassLocal.Checked = config.Content.BypassLocal;
            txtAdditionalExceptions.Text = config.Content.AdditionalExceptions;
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.AutoUpdate = chkAutoUpdate.Checked;
            chkCyclicChecking.Enabled = chkAutoUpdate.Checked;
            txtRetrytimeAfterEvent.Enabled = chkAutoUpdate.Checked;
            if (!chkAutoUpdate.Checked)
            {
                chkCyclicChecking.Checked = false;
            }
        }

        private void chkConsiderWinHTTP_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.ConsiderWinHTTP = chkConsiderWinHTTP.Checked;
        }

        private void chkCyclicChecking_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.CyclicCheck = chkCyclicChecking.Checked;
            txtRetrytimeAfterEvent.Enabled = !chkCyclicChecking.Checked;
        }

        private void txtRetrytimeAfterEvent_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRetrytimeAfterEvent.Text))
            {
                Configuration.RetryTimeSec = 0;
                return;
            }

            int.TryParse(txtRetrytimeAfterEvent.Text, out var retryTime);
            Configuration.RetryTimeSec = retryTime;
        }

        private void chkGitSupport_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.ConsiderGit = chkGitSupport.Checked;
            if (Configuration.ConsiderGit)
            {
                if (!CheckGitExecutable())
                {
                    //enter path to git
                    if (dlgSelectGitExecutable.ShowDialog() == DialogResult.OK)
                    {
                        Configuration.PathToGitExecutable = dlgSelectGitExecutable.FileName;
                    }
                }

                if (!CheckGitExecutable())
                {
                    chkGitSupport.Checked = false;
                    Configuration.ConsiderGit = false;
                }
            }
        }

        private bool CheckGitExecutable()
        {
            if (string.IsNullOrEmpty(Configuration.PathToGitExecutable))
            {
                return false;
            }

            if (!File.Exists(Configuration.PathToGitExecutable))
            {
                return false;
            }

            return true;
        }

    }

    class ProxyConfigWrapper
    {
        public ProxyConfig Content { get; }
        private Func<ProxyConfig> currentProxyConfig;
        public ProxyConfigWrapper(ProxyConfig content, Func<ProxyConfig> currentProxyConfig)
        {
            Content = content;
            this.currentProxyConfig = currentProxyConfig;
        }

        public override string ToString()
        {
            string result = Content.ToString();
            if (Content.Equals(currentProxyConfig.Invoke()))
            {
                result += " [active]";
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return Content.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }
    }
}
