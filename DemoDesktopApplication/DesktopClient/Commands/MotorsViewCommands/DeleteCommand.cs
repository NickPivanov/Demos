using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DesktopClient.Commands.MotorsViewCommands
{
    public class DeleteCommand : BaseCommand
    {
        private readonly IMotorService<Motor> motorService;
        private readonly ILogger logger;

        public DeleteCommand(IMotorService<Motor> service)
        {
            motorService = service;
            logger = App.serviceProvider.GetRequiredService<ILoggerProvider>().CreateLogger("DeleteCommand");
        }

        public override void Execute(object parameter)
        {
            int id = (int)parameter;
            logger.LogInformation("Deleting Motor");

            try
            {
                Task.Run(async () => 
                {
                    await motorService.DeleteAsync(id);
                }).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while deleting Motor with id={id}");
            }

            logger.LogInformation($"Motor (id={id}) deleted successfully");
            base.Execute(parameter);

        }
    }
}
