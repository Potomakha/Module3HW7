using Module3HW7.Configs;

namespace Module3HW7.Interfaces
{
    public interface IConfigurationService
    {
        public Config GetConfig();
        public string ReadConfigFile(string path);
    }
}
