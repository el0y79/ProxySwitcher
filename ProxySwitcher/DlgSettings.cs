using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProxySwitcher
{
    public partial class DlgSettings : Form
    {
        public Configuration Configuration { get; private set; }
        ConfigurationLoader loader = new ConfigurationLoader();
        public DlgSettings()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            Configuration=loader.LoadConfiguration();
            UpdateDialog();
        }

        private void UpdateDialog()
        {
            lstConfigurations.Items.Clear();
            Configuration.Proxies.ForEach(x => lstConfigurations.Items.Add(x));
            
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

            ProxyConfig config = (ProxyConfig)lstConfigurations.SelectedItem;
            Configuration.Proxies.Remove(config);
            lstConfigurations.Items.Remove(config);
        }

        private void lstConfigurations_SelectedValueChanged(object sender, EventArgs e)
        {
            ProxyConfig config = (ProxyConfig)lstConfigurations.SelectedItem;
            if (config == null)
            {
                return;
            }
            txtName.Text = config.Name;
            txtOwnIP.Text = config.OwnIP;
            txtProxy.Text = config.Proxy;
            chkBypassLocal.Checked = config.BypassLocal;
            txtAdditionalExceptions.Text = config.AdditionalExceptions;
        }

        private void DlgSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            loader.SaveConfiguration(Configuration);
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.AutoUpdate = chkAutoUpdate.Checked;
            chkCyclicChecking.Enabled = chkAutoUpdate.Checked;
            txtRetrytimeAfterEvent.Enabled = chkAutoUpdate.Checked;
            if(!chkAutoUpdate.Checked)
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
}
