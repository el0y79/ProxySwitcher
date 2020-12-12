using System;
using System.Collections.Generic;
using System.IO;

namespace ProxySwitcher
{
    public class ConfigurationLoader
    {
        private string fileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "configurations.json");
        private JsonAdapter<Configuration> jsonAdapter = new JsonAdapter<Configuration>();

        public Configuration LoadConfiguration()
        {
            if (!File.Exists(fileName))
                return new Configuration();
            try
            {
                return jsonAdapter.Restore(File.ReadAllText(fileName)) ?? new Configuration();
            }
            catch (Exception)
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