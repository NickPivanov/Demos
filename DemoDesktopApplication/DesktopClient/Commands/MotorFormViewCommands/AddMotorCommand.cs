using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DesktopClient.Commands.MotorFormViewCommands
{
    public class AddMotorCommand : BaseCommand
    {
        private readonly IMotorService<Motor> motorService;
        private readonly IMotorPropertyService<MotorProperty> propertyService;
        private readonly ILogger logger;

        public AddMotorCommand(IMotorService<Motor> service, IMotorPropertyService<MotorProperty> motorPropertyService)
        {
            motorService = service;
            propertyService = motorPropertyService;
            logger = App.serviceProvider.GetRequiredService<ILoggerProvider>().CreateLogger("AddMotorCommand");
        }

        public override void Execute(object parameter)
        {
            var motor = (parameter as Motor);
            logger.LogInformation("Creating new Motor");

            try
            {
                this.SetMotorProperties(propertyService, motor);
                Task.Run(() =>
                {
                    motorService.AddNewAsync(motor);
                }).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while adding new Motor");
            }

            logger.LogInformation($"{motor.Name} added");
            base.Execute(parameter);
        }
    }
}
