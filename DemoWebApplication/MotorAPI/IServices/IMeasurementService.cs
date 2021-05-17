using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Services
{
    public interface IMeasurementService<T> where T : class
    {
        public Task<IEnumerable<T>> GetMeasurementsAsync();
    }
}
