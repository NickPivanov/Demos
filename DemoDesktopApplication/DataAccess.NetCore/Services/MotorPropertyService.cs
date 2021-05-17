using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.NetCore.Services
{
    public class MotorPropertyService<T> : IMotorPropertyService<T> where T : MotorProperty
    {
        private readonly MotorDbContext db;
        public MotorPropertyService(MotorDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get properties collection for selected motor type
        /// </summary>
        /// <param name="motorType"></param>
        /// <returns>Returns IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetPropertiesForMotorTypeAsync(MotorType motorType)
        {
            return await db.MotorProperties.Where(p => p.MotorType == motorType)
                .Except(db.MotorProperties.Where(p => p.Name.Contains("Actual")))
                .Union(db.MotorProperties.Where(p => p.MotorType == null))
                .Cast<T>().ToListAsync();
        }
    }
}
