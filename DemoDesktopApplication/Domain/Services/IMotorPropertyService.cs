using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IMotorPropertyService<T>
    {
        public Task<IEnumerable<T>> GetPropertiesForMotorTypeAsync(MotorType motorType);
    }
}
