using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module3HW7.Interfaces;

namespace Module3HW7.Services
{
    public class MessageWriter : IMessageWriter
    {
        private readonly IConfigurationService _configuration;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly DirectoryInfo _directoryInfo;
        private StreamWriter _logWriter;
        private string _backupPath;

        public MessageWriter(IConfigurationService configurationService)
        {
            _configuration = configurationService;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Directory.GetCurrentDirectory() + "\\" + _configuration.GetConfig().BackupPath);
            _backupPath = stringBuilder.ToString();
            _directoryInfo = new DirectoryInfo(_backupPath);
            _directoryInfo.Create();
            stringBuilder.Clear();
            stringBuilder.Append(Directory.GetCurrentDirectory() + "\\" + _configuration.GetConfig().LoggerPath + "\\log.txt");
            var logFilePath = stringBuilder.ToString();
            _logWriter = new StreamWriter(logFilePath, true);
            _logWriter.AutoFlush = true;
        }

        public async Task WriteLogAsync(string message)
        {
            await _semaphore.WaitAsync();
            await _logWriter.WriteLineAsync(message);
            _semaphore.Release();
        }
    }
}
