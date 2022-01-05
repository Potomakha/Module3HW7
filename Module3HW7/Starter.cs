using System;
using System.Threading.Tasks;
using Module3HW7.Interfaces;
using Module3HW7.Services;

namespace Module3HW7
{
    public class Starter
    {
        private readonly ILogger _logger;
        private readonly IConfigurationService _configurationService;
        private readonly IMessageWriter _messageWriter;

        public Starter(ILogger logger, IConfigurationService configurationService, IMessageWriter messageWriter)
        {
            _logger = logger;
            _configurationService = configurationService;
            _messageWriter = messageWriter;
            _logger.MakeBackup += () => { Console.WriteLine("delaem"); };
        }

        public async Task Run()
        {
            Task task1 = Action("first");
            Task task2 = Action("second");
            await Task.WhenAll(task1, task2);
        }

        public async Task Action(string message)
        {
            for (int i = 0; i < 50; i++)
            {
                await _logger.LogErrorAsync(message);
            }
        }
    }
}