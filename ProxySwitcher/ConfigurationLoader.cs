using System;
using System.Collections.Generic;
using System.IO;
using TelegramUtils;

namespace ProxySwitcher
{
    public class ConfigurationLoader
    {
        private string fileName = "configurations.json";
        private JsonAdapter<Configuration> jsonAdapter = new JsonAdapter<Configuration>();

        public Configuration LoadConfiguration()
        {
            if (!File.Exists(fileName))
                return new Configuration();
            try
            {
                return jsonAdapter.Restore(File.ReadAllText(fileName)) ?? new Configuration();
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine("xxx");
                // ignored
            }

            return new Configuration();
        }

        public void SaveConfiguration(Configuration configuration)
        {
            JsonAdapter<Configuration> jsonAdapter = new JsonAdapter<Configuration>();
            File.WriteAllText(fileName, jsonAdapter.Dump(configuration));
        }

    }
}