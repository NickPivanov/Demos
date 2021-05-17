using Domain.Models;
using Domain.Services;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.ADONet.Services
{
    public class ADOMotorService<T> : IMotorService<T> where T : Motor
    {
        private ADOContext db;

        public ADOMotorService(ADOContext context)
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
            Exception exception = db.InsertAsync(entity).Exception;
            if (exception is not null)
                throw exception;

            await db.InsertAsync(entity);
            return await db.GetTable<T>()
                .Where(m => m.Name == entity.Name)
                .Where(m => m.Type == entity.Type)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <returns>Returns IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.GetTable<T>().ToListAsync();
        }

        /// <summary>
        /// Get entity from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns selected entity</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await db.GetTable<T>().FirstOrDefaultAsync(m => m.Id == id);
            if (entity is null)
            {
                throw new Exception($"Element id = {id} not found" );
            }
            
            return entity;
        }

        /// <summary>
        /// Delete entity from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Id of deleted entity</returns>
        public async Task<int> DeleteAsync(int id)
        {
            var motor = await db.GetTable<T>().FirstOrDefaultAsync(m => m.Id == id);
            if (motor is null)
                throw new Exception($"Motor id = {id} doesn't exist in database");

            await db.DeleteAsync(motor);
            return motor.Id;
        }

        /// <summary>
        /// Update edited entity in database
        /// </summary>
        /// <param name="edited_entity"></param>
        /// <returns>Returns updated entity</returns>
        public async Task<T> UpdateAsync(T edited_entity)
        {
            await db.UpdateAsync(edited_entity);
            var updated = await GetByIdAsync(edited_entity.Id);
            return updated;
        }
    }
}
