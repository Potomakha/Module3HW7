using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module3HW7.Configs;
using Module3HW7.Interfaces;

namespace Module3HW7.Services
{
    public class MessageWriter : IMessageWriter
    {
        private readonly IConfigurationService _configuration;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly DirectoryInfo _directoryInfo;
        private StreamWriter _logWriter;
        private string _loggerPath;
        private Config _config;

        public MessageWriter(IConfigurationService configurationService)
        {
            _configuration = configurationService;
            _config = _configuration.GetConfig();
            _loggerPath = Path.Combine(Directory.GetCurrentDirectory(), _config.LoggerPath);
            _directoryInfo = new DirectoryInfo(_loggerPath);
            _directoryInfo.Create();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), _config.LoggerPath, _config.LogFileName);
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
