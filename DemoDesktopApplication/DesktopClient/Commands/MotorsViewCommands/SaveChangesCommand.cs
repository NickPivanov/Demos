using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DesktopClient.Commands.MotorsViewCommands
{
    public class SaveChangesCommand : BaseCommand
    {
        private readonly IMotorService<Motor> motorService;
        private  ILogger logger;

        public SaveChangesCommand(IMotorService<Motor> service)
        {
            motorService = service;
            logger = App.serviceProvider.GetRequiredService<ILoggerProvider>()
                .CreateLogger("SaveChangesCommand");
        }

        public override void Execute(object parameter)
        {
            var motor = parameter as Motor;
            logger.LogInformation("Changing motor");

            if (motor != null)
            {
                try
                {
                    Task.Run(async () =>
                    {
                        await motorService.UpdateAsync(motor);
                    }).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while saving changes");
                }
            }
            logger.LogInformation("Motor changed");
            base.Execute(parameter);
        }
    }
}
