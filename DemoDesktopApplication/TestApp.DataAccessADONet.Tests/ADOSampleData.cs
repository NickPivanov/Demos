using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataAccessADONet.Tests
{
    public static class ADOSampleData
    {
        public static IEnumerable<Motor> GetSampleData()
        {
            List<MotorProperty> motorProperties = new()
            {
                new MotorProperty { Name = "Max Power (kW)" },
                new MotorProperty { Name = "Voltage (V)", MotorType = MotorType.Electric },
                new MotorProperty { Name = "Current (A)", MotorType = MotorType.Electric },
                new MotorProperty { Name = "Fuel Consumption per hour (l/h)", MotorType = MotorType.Combustion },
                new MotorProperty { Name = "Max Torque at (rpm)", MotorType = MotorType.Combustion },
                new MotorProperty { Name = "Max presure (bar)", MotorType = MotorType.Hydraulic },
                new MotorProperty { Name = "Displacement (cm3/rev)", MotorType = MotorType.Hydraulic },
                new MotorProperty { Name = "Actual Current", MotorType = MotorType.Electric },
                new MotorProperty { Name = "Actual Revs", MotorType = MotorType.Combustion },
                new MotorProperty { Name = "Actual Pressure", MotorType = MotorType.Hydraulic }
            };

            var actualCurrent = motorProperties.First(p => p.Name.Contains("Actual Current"));
            var actualRevs = motorProperties.First(p => p.Name.Contains("Actual Revs"));
            var actualPressure = motorProperties.First(p => p.Name.Contains("Actual Pressure"));

            var motor1 = new Motor()
            {
                Id = 0,
                Name = "Motor1",
                Type = MotorType.Electric,
                Characteristics = new List<Characteristic>
                    {
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Max Power")), Value = 2},
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Voltage")), Value = 230},
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Current (A)")), Value = 8.7}
                    },

                MeasurementsLog = new List<Measurement>
                    {
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 0, 0)), MotorProperty = actualCurrent, Value = 7.0},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 10, 0)), MotorProperty = actualCurrent, Value = 7.9},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 20, 0)), MotorProperty = actualCurrent, Value = 6.5},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 30, 0)), MotorProperty = actualCurrent, Value = 7.3},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 40, 0)), MotorProperty = actualCurrent, Value = 7.8},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 50, 0)), MotorProperty = actualCurrent, Value = 6.9},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(11, 0, 0)), MotorProperty = actualCurrent, Value = 7.0}
                    }
            };

            var motor2 = new Motor()
            {
                Id = 1,
                Name = "Motor2",
                Type = MotorType.Combustion,
                Characteristics = new List<Characteristic>
                    {
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Max Power")), Value = 50},
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Fuel Consumption")), Value = 4},
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Max Torque")), Value = 3000}
                    },

                MeasurementsLog = new List<Measurement>
                    {
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 0, 0)), MotorProperty = actualRevs, Value = 2890},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 10, 0)), MotorProperty = actualRevs, Value = 3100},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 20, 0)), MotorProperty = actualRevs, Value = 2800},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 30, 0)), MotorProperty = actualRevs, Value = 2860},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 40, 0)), MotorProperty = actualRevs, Value = 2875},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 50, 0)), MotorProperty = actualRevs, Value = 2790},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(11, 0, 0)), MotorProperty = actualRevs, Value = 2900}
                    }
            };

            var motor3 = new Motor()
            {
                Id = 2,
                Name = "Motor3",
                Type = MotorType.Hydraulic,
                Characteristics = new List<Characteristic>
                    {
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Max Power")), Value = 1},
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Max presure")), Value = 160},
                        new Characteristic{MotorProperty = motorProperties.First(p => p.Name.Contains("Displacement")), Value = 16}
                    },

                MeasurementsLog = new List<Measurement>
                    {
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 0, 0)), MotorProperty = actualPressure, Value = 155},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 10, 0)), MotorProperty = actualPressure, Value = 158},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 20, 0)), MotorProperty = actualPressure, Value = 140},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 30, 0)), MotorProperty = actualPressure, Value = 145},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 40, 0)),MotorProperty = actualPressure, Value = 159},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(10, 50, 0)), MotorProperty = actualPressure, Value = 160},
                        new Measurement{Time = DateTime.Now.Date.Add(new TimeSpan(11, 0, 0)), MotorProperty = actualPressure, Value = 139}
                    }
            };

            return new List<Motor>() {motor1, motor2, motor3 };
        }

        public static Motor AssignSmapleId(Motor motor)
        {
            int id = GetSampleData().Count();
            motor.Id = id;
            return motor;
        }
    }
}
