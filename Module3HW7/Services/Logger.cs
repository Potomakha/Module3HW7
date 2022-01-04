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
        private static int _recordsCount = 0;
        private Config _config;

        public Logger(IMessageWriter messageWriter, IConfigurationService configurationService)
        {
            _messageWriter = messageWriter;
            _configurationService = configurationService;
            _config = _configurationService.GetConfig();
        }

        public event Action MakeBackup;

        public async Task Log(LogType type, string mesage)
        {
            var log = $"{DateTime.UtcNow}: {type}: {mesage}";
            Console.WriteLine(log);
            await _messageWriter.WriteLog(log);
            _recordsCount++;

            if (_recordsCount >= _config.RecordsInOneTime)
            {
                MakeBackup.Invoke();
                _recordsCount = 0;
            }
        }

        public async Task LogError(string message)
        {
            await Log(LogType.Error, message);
        }

        public async Task LogInfo(string message)
        {
            await Log(LogType.Info, message);
        }

        public async Task LogWarning(string message)
        {
            await Log(LogType.Warning, message);
        }
    }
}
