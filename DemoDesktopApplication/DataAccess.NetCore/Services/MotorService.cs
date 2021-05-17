using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.NetCore.Services
{
    public class MotorService<T> : IMotorService<T> where T : Motor
    {
        private readonly MotorDbContext db;

        public MotorService(MotorDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns added entity with Id</returns>
        public async Task<T> AddNewAsync(T entity)
        {
            if (entity is null)
                throw new Exception("Motor couldn't be null");
            if (string.IsNullOrEmpty(entity.Name))
                throw new Exception("Motor name couldn't be null or empty");

            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete entity from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Id of deleted entity</returns>
        public async Task<int> DeleteAsync(int id)
        {
            var motor = await db.Set<T>().Include(c => c.Characteristics).FirstOrDefaultAsync(m => m.Id == id);
            if (motor is null)
                throw new Exception($"Motor id = {id} doesn't exist in database");

            db.Set<T>().Remove(motor);
            await db.SaveChangesAsync();
            return id;
        }

        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <returns>Returns IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await db.Set<T>()
                .Include(c => c.Characteristics)
                .Include(m => m.MeasurementsLog)
                .ToListAsync();
            return result;
        }

        /// <summary>
        /// Get entity from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns selected entity</returns>
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

        /// <summary>
        /// Update edited entity in database
        /// </summary>
        /// <param name="edited_entity"></param>
        /// <returns>Returns updated entity</returns>
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
