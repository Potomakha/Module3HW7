using System;
using System.Threading.Tasks;

namespace Module3HW7.Interfaces
{
    public interface ILogger : IAsyncDisposable
    {
        event Func<string, Task> OnBackedUp;
        Task LogInfo<T>(T message);
        Task LogError<T>(T message);
    }
}
