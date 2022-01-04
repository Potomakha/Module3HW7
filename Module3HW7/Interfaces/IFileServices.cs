using System.Threading.Tasks;
using System;
using Module3HW7.Configs;

namespace Module3HW7.Interfaces
{
    public interface IFileServices
    {
        Task<bool> WriteLine(IAsyncDisposable streamWriter, string text);
        Task<string> ReadAllTextOrNull(string path);
        Task ConfigureLoggerDirectory(LoggerConfig loggerConfig);
        Task ConfigureBackupDirectory(BackupConfig backupConfig);
        IAsyncDisposable GetStreamWriterInstance(string filePath);
        Task DisposeAsync(IAsyncDisposable streamWriter);
        Task<bool> WriteAllText(string path, string text);
    }
}