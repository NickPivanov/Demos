using AccountsDepartment.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HRAgencyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ExpensesController : ControllerBase
    {
        private readonly IPersonelExpenseService<EmployeeBase> _personelExpenseService;

        public ExpensesController(IPersonelExpenseService<EmployeeBase> personelExpenseService)
        {
            _personelExpenseService = personelExpenseService;
        }

        /// <summary>
        /// Calculate salary for Employee on selected date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <response code="200">Seccesfully</response>
        /// <response code="400">Error on calculating process</response>
        [HttpGet("Calculate/{id}", Name = "CalculateSalary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<double> CalculateSalaryFor(int id, DateTime date)
        {
            if (string.IsNullOrEmpty(date.ToString()))
                return BadRequest();

            var salary = _personelExpenseService.CalculateSalaryForEmployee(id, date.Date);
            return Ok(salary);
        }

        /// <summary>
        /// Get current salary for selected Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Seccesfully</response>
        /// <response code="400">Error getting data from database</response>
        [HttpGet("Current/{id}",Name = "GetCurrentSalaryForEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<double> GetCurrentSalaryForEmployee(int id)
        {
            double? currentSalary = _personelExpenseService.GetEmployeeCurrentSalary(id);
            if (!currentSalary.HasValue)
                return BadRequest();

            return Ok(currentSalary);
        }

        /// <summary>
        /// Calculate total personel expenses
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Seccesfully</response>
        /// <response code="400">Error getting data from database</response>
        [HttpGet("Total", Name = "GetTotalPersonelExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<double> GetTotalPersonelExpenses()
        {
            var total = _personelExpenseService.GetTotalPersonelExpenses();
            return Ok(total);
        }

        /// <summary>
        /// Save value of current salary to database for selected employee.
        /// </summary>
        /// <param name="employee_id"></param>
        /// <param name="currentSalary"></param>
        /// <returns></returns>
        /// <response code="200">Seccesfully saved</response>
        /// <response code="400">Error while adding data to database</response>
        [HttpPost("/Set", Name = "SetEmployeeCurrentSalary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SetEmployeeCurrentSalary(int employee_id, double currentSalary)
        {
            try
            {
                await _personelExpenseService.SetEmployeeCurrentSalary(employee_id, currentSalary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}
