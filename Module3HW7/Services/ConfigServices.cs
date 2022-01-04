using System.IO;
using System.Threading.Tasks;
using Module3HW7.Configs;
using Module3HW7.Interfaces;
using Newtonsoft.Json;

namespace Module3HW7.Services
{
    public class ConfigServices : IConfigServices
    {
        private const string ConfigPath = "appsettings.json";
        private readonly IFileServices _fileServices;
        private Config _config;
        public ConfigServices(IFileServices fileServices)
        {
            _fileServices = fileServices;
            Init().GetAwaiter().GetResult();
        }

        public Config Config => _config;
        public LoggerConfig LoggerConfig => _config.LoggerConfig;
        public BackupConfig BackupConfig => _config.BackupConfig;

        private async Task Init()
        {
            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath);
            }
            else
            {
                var configFile = await _fileServices.ReadAllTextOrNull(ConfigPath);
                _config = JsonConvert.DeserializeObject<Config>(configFile);
            }
        }
    }
}
