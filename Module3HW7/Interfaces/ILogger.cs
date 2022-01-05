using System;
using System.Threading.Tasks;
using Module3HW7.Models;

namespace Module3HW7.Interfaces
{
    public interface ILogger
    {
        public event Action MakeBackup;
        public Task LogInfoAsync(string message);
        public Task LogErrorAsync(string message);
        public Task LogWarningAsync(string message);
        public Task LogAsync(LogType type, string mesage);
    }
}
