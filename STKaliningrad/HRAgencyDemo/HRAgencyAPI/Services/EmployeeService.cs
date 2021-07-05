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
    /// <summary>
    /// Provides access to CRUD operations with Employee
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EmployeeService<T> : IEmployeeService<T> where T : EmployeeBase
    {
        protected readonly HRAgencyDBContext db;
        public EmployeeService(HRAgencyDBContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <returns>IEnumerable of objects from database</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get entity by Id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity of type T</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var result = await db.Set<T>().FindAsync(id);
            if (result is null)
            {
                throw new Exception("Employee not found");
            }
            return (T)result;
        }

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Id of a new entity</returns>
        public async Task<int> AddNewAsync(T employee)
        {
            var e = await db.Set<T>().AddAsync(employee);
            await db.SaveChangesAsync();
            return e.Entity.Id;
        }

        /// <summary>
        /// Delete selected by Id entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var e = await GetByIdAsync(id);
            db.Set<T>().Remove(e);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="edited_employee"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T edited_employee)
        {
            db.Set<T>().Update(edited_employee);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of employee's subordinates.
        /// Throws exception if selected employee can't have subordinates
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Subordinates list of type T</returns>
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

        /// <summary>
        /// Assign new subordinate to selected employee
        /// </summary>
        /// <param name="id">Lead Id</param>
        /// <param name="subordinateId">New subordinate Id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove selected subordinate from selected empployee
        /// </summary>
        /// <param name="id">Lead Id</param>
        /// <param name="subordinateId">Subordinate Id</param>
        /// <returns></returns>
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
