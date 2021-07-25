using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class CarService<T> : ISalesService<T> where T : Car
    {
        private readonly SalesDbContext db;
        public CarService(SalesDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get list of Cars including Orders
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            var result = await db.Set<T>().Include(o => o.CarOrders).AsNoTracking().ToListAsync();
            
            if (result is null)
                throw new Exception("No records in database");

            return result;
        }

        /// <summary>
        /// Get the Car by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var car = await db.Set<T>()
                .Include(o => o.CarOrders)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(car is null)
                throw new Exception("Object doesn't exist in database");

            return car;
        }
    }
}
