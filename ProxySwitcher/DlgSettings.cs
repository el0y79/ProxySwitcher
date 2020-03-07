using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelegramUtils;

namespace ProxySwitcher
{
    public partial class DlgSettings : Form
    {
        public List<ProxyConfig> Configurations { get; } = new List<ProxyConfig>();
        ConfigurationLoader loader = new ConfigurationLoader();
        public DlgSettings()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            Configurations.Clear();
            Configurations.AddRange(loader.LoadConfiguration());
            UpdateList();
        }

        private void UpdateList()
        {
            lstConfigurations.Items.Clear();
            Configurations.ForEach(x => lstConfigurations.Items.Add(x));
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

            var config = Configurations.FirstOrDefault(x => x.Name.Equals(txtName.Text));
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
                Configurations.Add(config);
                UpdateList();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstConfigurations.SelectedItem == null)
            {
                return;
            }

            ProxyConfig config = (ProxyConfig)lstConfigurations.SelectedItem;
            Configurations.Remove(config);
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
            loader.SaveConfiguration(Configurations);
        }
    }
}
