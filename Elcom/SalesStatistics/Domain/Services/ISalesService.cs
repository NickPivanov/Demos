using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISalesService<T>
    {
        public Task<T> GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
    }
}
