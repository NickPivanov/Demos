using Microsoft.EntityFrameworkCore;
using MotorAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Models.Services
{
    public class MotorService<T> : IMotorService<T> where T : Motor
    {
        private readonly MotorDbContext db;

        public MotorService(MotorDbContext context)
        {
            db = context;
        }

        public async Task<T> AddNewAsync(T entity)
        {
            if (entity is null)
                throw new Exception("Motor couldn't be null");
            if(string.IsNullOrEmpty(entity.Name))
                throw new Exception("Motor name couldn't be null or empty");

            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var motor = await db.Set<T>().Include(c => c.Characteristics).FirstOrDefaultAsync(m => m.Id == id);
            if (motor is null)
                throw new Exception("Motor doesn't exist or null");

            db.Set<T>().Remove(motor);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await db.Set<T>()
                .Include(c => c.Characteristics)
                .Include(m => m.MeasurementsLog)
                .ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var motor = await db.Set<T>()
                .Include(c => c.Characteristics)
                .Include(m => m.MeasurementsLog)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motor is null)
                throw new Exception("Object doesn't exist in database");

            return motor;
        }

        public async Task<T> UpdateAsync(T edited_entity)
        {
            var entity = await GetByIdAsync(edited_entity.Id);
            if (entity is null)
                throw new Exception($"{edited_entity.Name} doesn't exist in database");
            
            db.Set<T>().Update(edited_entity);
            await db.SaveChangesAsync();

            return edited_entity;

        }
    }
}
