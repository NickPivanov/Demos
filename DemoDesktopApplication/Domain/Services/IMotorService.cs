using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IMotorService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> AddNewAsync(T entity);
        public Task<T> UpdateAsync(T edited_entity);
        public Task<int> DeleteAsync(int id);
    }
}
