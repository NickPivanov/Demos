using MotorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.IServices
{
    public interface IMotorFactory
    {
        public Task<Motor> CreateNewMotorForType(MotorType motorType);
    }
}
