using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAgencyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService<EmployeeBase> employeeService;
        public EmployeeController(IEmployeeService<EmployeeBase> service)
        {
            employeeService = service;
        }

        /// <summary>
        /// Get the Employee by Id
        /// </summary>
        /// <param name="id">Uniq identifier</param>
        /// <returns></returns>
        /// <response code="200">Returns the Employee</response>
        /// <response code="404">Unable to find a Employee by the requested id</response>
        [HttpGet("{id}", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeBase>> GetById(int id)
        {
            var employee = await employeeService.GetByIdAsync(id);
            if (employee is null)
                return NotFound();

            var result = employee.Group switch
            {
                EmployeeGroup.Employees => employee as Employee,
                EmployeeGroup.Management => employee as Manager,
                EmployeeGroup.Sales => employee as Salesman,
                _ => employee
            };

            return Ok(result);
        }

        /// <summary>
        /// Get all Employees from database
        /// </summary>
        /// <returns>IEnumerable collection of all objects</returns>
        /// <response code="200">Returns collection of Employees</response>
        /// <response code="404">Database has no items</response>
        [HttpGet(Name = "GetAllEmplyees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EmployeeBase>>> GetAllEmployees()
        {
            var employees = await employeeService.GetAllAsync();
            if (employees is null)
                return NotFound();

            return Ok(employees.ToList());
        }

        /// <summary>
        /// Get Employee's subordinates
        /// </summary>
        /// <returns>List of subordinates</returns>
        /// <response code="200">Returns collection of Employees</response>
        /// <response code="404">Error on getting data</response>
        [HttpGet("subordinates/{id}", Name = "GetSubordinatesForEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EmployeeBase>>> GetSubordinatesForEmployee(int id)
        {
            var subordinates = await employeeService.GetSubordinatesForEmployee(id);
            if (subordinates is null)
                return NotFound();

            return Ok(subordinates.ToList());
        }

        /// <summary>
        /// Assign subordinate for Employee
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Error on setting data</response>
        [HttpPost("subordinates/{id}", Name = "AssignSubordinateForEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AssignSubordinateForEmployee(int id, int subordinateId)
        {
            await employeeService.AssignSubordinateForEmployee(id, subordinateId);
            
            return Ok();
        }

        /// <summary>
        /// Remove subordinate from Employee's subordinates list
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Error on updating data</response>
        [HttpDelete("subordinates/{id}", Name = "RemoveSubordinateForEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveSubordinateForEmployee(int id, int subordinateId)
        {
            await employeeService.RemoveSubordinateForEmployee(id, subordinateId);

            return Ok();
        }

        /// <summary>
        /// Add new EmployeeBase object to database. 
        /// Values for properties Name, Group and EmployedDate are required.
        /// Group values are:
        /// 0 - Employee
        /// 1 - Manager
        /// 2 - Salesman
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="employedDate"></param>
        /// <returns></returns>
        /// <response code="200">Seccesful result of procedure</response>
        /// <response code="400">Error while adding new object to database</response>
        [HttpPost(Name = "AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeBase>> AddNewEmployee(string name, EmployeeGroup group, DateTime employedDate)
        {
            if (name.Length > 50)
                ModelState.AddModelError("Name", "Employee name should be less than 50 char.");
            if (string.IsNullOrEmpty(group.ToString()))
                ModelState.AddModelError("Group", "Employee group should not be undefined");
            if (string.IsNullOrEmpty(employedDate.ToString()))
                ModelState.AddModelError("EmployedDate", "Employed Date should not be empty");
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                EmployeeBase e = new EmployeeFactory(group).CreateEmployee(name, employedDate);
                await employeeService.AddNewAsync(e);
                return Ok(e);
            }
        }

        /// <summary>
        /// Sends a request on updating selected Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <response code="200">Seccesfully updated</response>
        /// <response code="400">Error while updating data</response>
        [HttpPut(Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeBase>> UpdateEmployee([FromBody] EmployeeBase employee)
        {
            if (employee is null)
                return BadRequest();

            await employeeService.UpdateAsync(employee);
            return Ok(employee);
        }

        /// <summary>
        /// Delete selected Employee from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Deleted successfully</response>
        /// <response code="400">Error while deleting object from database</response>
        [HttpDelete("{id}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var e = await employeeService.GetByIdAsync(id);
            if (e is null)
                return BadRequest();
            await employeeService.DeleteAsync(id);
            return Ok();
        }


    }
}
