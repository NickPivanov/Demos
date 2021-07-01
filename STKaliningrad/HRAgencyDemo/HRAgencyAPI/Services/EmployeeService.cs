using DataAccess;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAgencyAPI.Services
{
    public class EmployeeService<T> : IEmployeeService<T> where T : EmployeeBase
    {
        protected readonly HRAgencyDBContext db;
        public EmployeeService(HRAgencyDBContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await db.Set<T>().FindAsync(id);
            if (result is null)
            {
                throw new Exception("Employee not found");
            }
            return (T)result;
        }

        public async Task<int> AddNewAsync(T employee)
        {
            var e = await db.Set<T>().AddAsync(employee);
            await db.SaveChangesAsync();
            return e.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var e = await GetByIdAsync(id);
            db.Set<T>().Remove(e);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T edited_employee)
        {
            db.Set<T>().Update(edited_employee);
            await db.SaveChangesAsync();
        }

        public async Task<List<T>> GetSubordinatesForEmployee(int id)
        {
            var employees = await db.Set<T>().ToListAsync();
            var lead = employees.FirstOrDefault(n => n.Id == id);
            
            var result = lead.Group switch
            {
                EmployeeGroup.Management => (lead as Manager).Subordinates.ToList(),
                EmployeeGroup.Sales => (lead as Salesman).Subordinates.ToList(),
                _ => throw new Exception("Employee can't have subordinates")
            };

            return result.Cast<T>().ToList();
        }

        public async Task AssignSubordinateForEmployee(int id, int subordinateId)
        {
            var employees = await db.Set<T>().ToListAsync();
            var lead = employees.FirstOrDefault(n => n.Id == id);

            var subordinate = await db.Set<T>().FindAsync(subordinateId);
            switch(lead.Group)
            {
                case EmployeeGroup.Management: (lead as Manager).Subordinates.Add(subordinate);
                    await db.SaveChangesAsync();
                    break;
                case EmployeeGroup.Sales: (lead as Salesman).Subordinates.Add(subordinate);
                    await db.SaveChangesAsync();
                    break;
            }

        }

        public async Task RemoveSubordinateForEmployee(int id, int subordinateId)
        {
            var employees = await db.Set<T>().ToListAsync();
            var lead = employees.FirstOrDefault(n => n.Id == id);
            switch (lead.Group)
            {
                case EmployeeGroup.Management:
                    (lead as Manager).Subordinates.RemoveAll(s => s.Id == subordinateId);
                    await db.SaveChangesAsync();
                    break;
                case EmployeeGroup.Sales:
                    (lead as Salesman).Subordinates.RemoveAll(s => s.Id == subordinateId);
                    await db.SaveChangesAsync();
                    break;
            }
        }
    }
}
