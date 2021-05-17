using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.NetCore.Services
{
    public class MeasurementService<T> : IMeasurementService<T> where T : Measurement
    {
        private readonly MotorDbContext db;

        public MeasurementService(MotorDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get all measurements from database
        /// </summary>
        /// <returns>Returns IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetMeasurementsAsync()
        {
            var m = await db.Set<T>()
                .Include(p => p.MotorProperty)
                .Include(m => m.Motor).ToListAsync();
            return m;
        }
    }
}
