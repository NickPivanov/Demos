using Microsoft.EntityFrameworkCore;
using MotorAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Models.Services
{
    public class MotorPropertyService<T> : IMotorPropertyService<T> where T : MotorProperty
    {
        private readonly MotorDbContext db;
        public MotorPropertyService(MotorDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<T>> GetPropertiesForMotorTypeAsync(MotorType motorType)
        {
            return await db.MotorProperties.Where(p => p.MotorType == motorType)
                .Except(db.MotorProperties.Where(p => p.Name.Contains("Actual")))
                .Union(db.MotorProperties.Where(p => p.MotorType == null))
                .Cast<T>().ToListAsync();
        }
    }
}
