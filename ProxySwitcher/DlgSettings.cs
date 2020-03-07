using System;
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
            chkConsiderWinHTTP.Checked = Configuration.ConsiderWinHTTP;
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
            }
            else
            {
                config = new ProxyConfig
                {
                    Name = txtName.Text,
                    OwnIP = txtOwnIP.Text,
                    Proxy = txtProxy.Text
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
        }

        private void DlgSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            loader.SaveConfiguration(Configuration);
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.AutoUpdate = chkAutoUpdate.Checked;
        }

        private void chkConsiderWinHTTP_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.ConsiderWinHTTP = chkConsiderWinHTTP.Checked;
        }
    }
}
