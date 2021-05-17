using MotorAPI.IServices;
using MotorAPI.Models;
using MotorAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI
{
    public class MotorFactory : IMotorFactory
    {
        private IMotorPropertyService<MotorProperty> motorPropertyService;
        public MotorFactory(IMotorPropertyService<MotorProperty> service)
        {
            motorPropertyService = service;
        }

        public async Task<Motor> CreateNewMotorForType(MotorType motorType)
        {
            List<MotorProperty> properties = (await motorPropertyService.GetPropertiesForMotorTypeAsync(motorType)).ToList();

            List<Characteristic> characteristics = new();
            foreach (var p in properties)
                characteristics.Add(new Characteristic { MotorProperty = p, Value = 0 });

            Motor motor = new() { Name = "new", Type = motorType, Characteristics = characteristics };

            return motor;
        }
    }
}
