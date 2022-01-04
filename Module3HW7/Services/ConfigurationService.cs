using System;
using System.IO;
using Module3HW7.Configs;
using Module3HW7.Interfaces;
using Newtonsoft.Json;

namespace Module3HW7.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string ConfigurationPath = "config.json";

        public Config GetConfig()
        {
            var configfile = ReadConfigFile(ConfigurationPath);
            var config = JsonConvert.DeserializeObject<Config>(configfile);

            return config;
        }

        public string ReadConfigFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return File.ReadAllText(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }


    }
}
