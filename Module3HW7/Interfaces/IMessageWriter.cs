using System.Threading.Tasks;

namespace Module3HW7.Interfaces
{
    public interface IMessageWriter
    {
        public Task WriteLogAsync(string message);
    }
}
