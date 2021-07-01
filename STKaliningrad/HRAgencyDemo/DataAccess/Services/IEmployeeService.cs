using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IEmployeeService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<List<T>> GetSubordinatesForEmployee(int id);
        public Task AssignSubordinateForEmployee(int id, int subordinateId);
        public Task RemoveSubordinateForEmployee(int id, int subordinateId);
        public Task<int> AddNewAsync(T employee);
        public Task UpdateAsync(T edited_employee);
        public Task DeleteAsync(int id);
    }
}
