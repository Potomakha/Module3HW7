using Module3HW7.Configs;

namespace Module3HW7.Interfaces
{
    public interface IConfigServices
{
        LoggerConfig LoggerConfig { get; }
        BackupConfig BackupConfig { get; }
    }
}
