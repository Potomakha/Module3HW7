using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Module3HW7.Interfaces;
using Module3HW7.Services;

namespace Module3HW7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<IMessageWriter, MessageWriter>()
                .AddTransient<IConfigurationService, ConfigurationService>()
                .AddTransient<IBackupService, BackupService>()
                .AddTransient<Starter>()
                .BuildServiceProvider();
            var starter = serviceProvider.GetService<Starter>();
            Task.Run(async () => { await starter.Run(); }).GetAwaiter().GetResult();
        }
    }
}
