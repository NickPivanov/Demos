using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class MotorDbContextExtensions
    {
        public static void EnsureSeedPropertiesTable(this MotorDbContext context)
        {
            if (!context.Set<MotorProperty>().Any())
            {
                context.Set<MotorProperty>().AddRange(
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
                    );
                context.SaveChanges();
            }
        }

        public static void EnsureSeedMotorsData(this MotorDbContext context)
        {
            if (!context.Set<Motor>().Any())
            {
                var actualCurrent = context.MotorProperties.First(p => p.Name.Contains("Actual Current"));
                var actualRevs = context.MotorProperties.First(p => p.Name.Contains("Actual Revs"));
                var actualPressure = context.MotorProperties.First(p => p.Name.Contains("Actual Pressure"));

                var motor1 = new Motor()
                {
                    Name = "Motor1",
                    Type = MotorType.Electric,
                    Characteristics = new List<Characteristic>
                    {
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Max Power")), Value = 2},
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Voltage")), Value = 230},
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Current (A)")), Value = 8.7}
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
                    Name = "Motor2",
                    Type = MotorType.Combustion,
                    Characteristics = new List<Characteristic>
                    {
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Max Power")), Value = 50},
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Fuel Consumption")), Value = 4},
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Max Torque")), Value = 3000}
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
                    Name = "Motor3",
                    Type = MotorType.Hydraulic,
                    Characteristics = new List<Characteristic>
                    {
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Max Power")), Value = 1},
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Max presure")), Value = 160},
                        new Characteristic{MotorProperty = context.MotorProperties.First(p => p.Name.Contains("Displacement")), Value = 16}
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

                context.Set<Motor>().AddRange(motor1, motor2, motor3);

                context.SaveChanges();
            }
        }
    }
}
