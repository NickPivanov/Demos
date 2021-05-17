using MotorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Services
{
    public interface IMotorPropertyService<T>
    {
        public Task<IEnumerable<T>> GetPropertiesForMotorTypeAsync(MotorType motorType);
    }
}
