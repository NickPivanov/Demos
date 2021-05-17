using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Services
{
    public interface IMotorService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> AddNewAsync(T entity);
        public Task<T> UpdateAsync(T edited_entity);
        public Task DeleteAsync(int id);

    }
}
