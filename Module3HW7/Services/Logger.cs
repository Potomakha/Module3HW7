using System;
using System.Threading;
using System.Threading.Tasks;
using Module3HW7.Configs;
using Module3HW7.Interfaces;
using Module3HW7.Models;

namespace Module3HW7.Services
{
    public class Logger : ILogger
    {
        private readonly IMessageWriter _messageWriter;
        private readonly IConfigurationService _configurationService;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private static int _recordsCount = 0;
        private Config _config;

        public Logger(IMessageWriter messageWriter, IConfigurationService configurationService)
        {
            _messageWriter = messageWriter;
            _configurationService = configurationService;
            _config = _configurationService.GetConfig();
        }

        public event Func<Task> MakeBackup;

        public async Task LogAsync(LogType type, string mesage)
        {
            await _semaphoreSlim.WaitAsync();
            if (_recordsCount >= _config.RecordsInOneTime)
            {
                _recordsCount = 0;
                MakeBackup?.Invoke().GetAwaiter().GetResult();
            }

            _recordsCount++;
            _semaphoreSlim.Release();
            var log = $"{DateTime.UtcNow}: {type}: {mesage}";
            Console.WriteLine(log);
            await _messageWriter.WriteLogAsync(log);
        }

        public async Task LogErrorAsync(string message)
        {
            await LogAsync(LogType.Error, message);
        }

        public async Task LogInfoAsync(string message)
        {
            await LogAsync(LogType.Info, message);
        }

        public async Task LogWarningAsync(string message)
        {
            await LogAsync(LogType.Warning, message);
        }
    }
}
