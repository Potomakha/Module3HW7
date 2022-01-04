using System;
using Module3HW7.Interfaces;
using Module3HW7.Models;

namespace Module3HW7.Services
{
    public class Logger : ILogger
    {
        public static readonly Action MakeBackup;
        private readonly IMessageWriter _messageWriter;
        private readonly IConfigurationService _config;
        private int _recordsCount = 0;
        private int _maxRecords;
        public Logger(IMessageWriter messageWriter, IConfigurationService configurationService)
        {
            _messageWriter = messageWriter;
            _config = configurationService;
            _maxRecords = _config.GetConfig().RecordsInOneTime;
        }

        public void Log(LogType type, string mesage)
        {
            var log = $"{DateTime.UtcNow}: {type}: {mesage}";
            Console.WriteLine(log);
            _messageWriter.Write(mesage);
            _recordsCount++;

            if (_recordsCount >= _maxRecords)
            {
                MakeBackup.Invoke();
                _recordsCount = 0;
            }
        }

        public void LogError(string message)
        {
            Log(LogType.Error, message);
        }

        public void LogInfo(string message)
        {
            Log(LogType.Info, message);
        }

        public void LogWarning(string message)
        {
            Log(LogType.Warning, message);
        }
    }
}
