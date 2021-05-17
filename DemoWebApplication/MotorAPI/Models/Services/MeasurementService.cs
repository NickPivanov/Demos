using Microsoft.EntityFrameworkCore;
using MotorAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorAPI.Models.Services
{
    public class MeasurementService<T> : IMeasurementService<T> where T : Measurement
    {
        private readonly MotorDbContext db;

        public MeasurementService(MotorDbContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<T>> GetMeasurementsAsync()
        {
            var m = await db.Set<T>()
                .Include(p => p.MotorProperty)
                .Include(m => m.Motor).ToListAsync();
            return m;
        }
    }
}
