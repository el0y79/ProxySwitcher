using System.Collections.Generic;
using System.IO;
using TelegramUtils;

namespace ProxySwitcher
{
    public class ConfigurationLoader
    {
        private string fileName = "configurations.json";
        public List<ProxyConfig> LoadConfiguration()
        {
            List<ProxyConfig> configurations = new List<ProxyConfig>();
            if (!File.Exists(fileName))
                return new List<ProxyConfig>();
            JsonAdapter<List<ProxyConfig>> jsonAdapter = new JsonAdapter<List<ProxyConfig>>();
            configurations.Clear();
            configurations.AddRange(jsonAdapter.Restore(File.ReadAllText(fileName)));
            return configurations;
        }

        public void SaveConfiguration(List<ProxyConfig> configurations)
        {
            JsonAdapter<List<ProxyConfig>> jsonAdapter = new JsonAdapter<List<ProxyConfig>>();
            File.WriteAllText(fileName, jsonAdapter.Dump(configurations));

        }

    }
}