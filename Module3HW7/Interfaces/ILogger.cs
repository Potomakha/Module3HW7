using System;
using System.Threading.Tasks;
using Module3HW7.Models;

namespace Module3HW7.Interfaces
{
    public interface ILogger
    {
        public event Action MakeBackup;
        public Task LogInfo(string message);
        public Task LogError(string message);
        public Task LogWarning(string message);
        public Task Log(LogType type, string mesage);
    }
}
