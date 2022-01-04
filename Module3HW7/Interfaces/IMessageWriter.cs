using System.Threading.Tasks;

namespace Module3HW7.Interfaces
{
    public interface IMessageWriter
    {
        public Task WriteLog(string message);
        public Task WriteBackup();
    }
}
