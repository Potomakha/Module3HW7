using System.Threading.Tasks;

namespace Module3HW7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var startup = new Startup();
            await startup.Run();
        }
    }
}
