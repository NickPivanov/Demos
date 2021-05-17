using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopClient.Commands.MotorFormViewCommands
{
    public static class AddMotorCommandExtensions
    {
        public static void SetMotorProperties(this AddMotorCommand addMotorCommand, IMotorPropertyService<MotorProperty> propertyService, Motor motor)
        {
            List<MotorProperty> properties = new();
            
            var task = Task.Run(async () =>
            {
                properties = (await propertyService.GetPropertiesForMotorTypeAsync(motor.Type)).ToList();
            });
            task.Wait();

            if(task.IsCompletedSuccessfully)
            {
                List<Characteristic> characteristics = new();
                foreach (var p in properties)
                    characteristics.Add(new Characteristic { MotorProperty = p, Value = 0 });
                motor.Characteristics = characteristics;
            }
            
            
        }
    }
}
