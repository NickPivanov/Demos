using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class OrderService<T> : ISalesService<T> where T : Order
    {
        private readonly SalesDbContext db;
        public OrderService(SalesDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get all Orders from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            var result = await db.Set<T>().Include(c => c.Car).AsNoTracking().ToListAsync();

            if (result is null)
                throw new Exception("No records in database");

            return result;
        }

        /// <summary>
        /// Get the Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var order = await db.Set<T>().Include(c => c.Car)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                throw new Exception("Object doesn't exist in database");

            return order;
        }
    }
}
