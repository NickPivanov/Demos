using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotorAPI.IServices;
using MotorAPI.Models;
using MotorAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class MotorsController : ControllerBase
    {
        private readonly IMotorService<Motor> motorService;
        private readonly IMotorFactory motorFactory;
        private readonly ILogger<MotorsController> logger;
        public MotorsController(IMotorService<Motor> service, IMotorFactory factory, ILogger<MotorsController> Logger)
        {
            motorService = service;
            motorFactory = factory;
            logger = Logger;
        }

        /// <summary>
        /// Get all Motors from database
        /// </summary>
        /// <returns>IEnumerable collection of all objects</returns>
        /// <response code="200">Returns collection of Motors</response>
        /// <response code="404">Database has no items</response>
        [HttpGet(Name = "GetAllMotors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Motor>>> GetAll()
        {
            var motors = await motorService.GetAllAsync();
            if (motors is null)
                return NotFound();

            return Ok(motors.ToList());
        }

        /// <summary>
        /// Get the Motor by Id
        /// </summary>
        /// <param name="id">Uniq identifier</param>
        /// <returns></returns>
        /// <response code="200">Returns the Motor</response>
        /// <response code="404">Unable to find a Motor by the requested id</response>
        [HttpGet("{id}", Name = "GetMotorById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Motor>> GetById(int id)
        {
            var motor = await motorService.GetByIdAsync(id);
            if (motor is null)
                return NotFound();

            return Ok(motor);
        }

        /// <summary>
        /// Add new Motor object to database. 
        /// Values for properties Name and Type are required.
        /// Type values are:
        /// 0 - Electric
        /// 1 - Hydraulic
        /// 2 - Combustion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <response code="200">Seccesful result of procedure</response>
        /// <response code="400">Error while adding new object to database</response>
        [HttpPost(Name = "AddMotor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Motor>> AddMotor(string name, MotorType type)
        {
            if (name.Length > 50)
                ModelState.AddModelError("Name", "Motor name should be less than 50 char.");
            if (string.IsNullOrEmpty(type.ToString()))
                ModelState.AddModelError("Type", "Motor type should not be undefined");
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                Motor motor = await motorFactory.CreateNewMotorForType(type);
                motor.Name = name;
                await motorService.AddNewAsync(motor);
                return Ok(motor);
            }
        }

        /// <summary>
        /// Sends a request on updating selected Motor
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        /// <response code="200">Seccesfully updated</response>
        /// <response code="400">Error while updating data</response>
        [HttpPut(Name = "UpdateMotor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Motor>> UpdateMotor([FromBody]Motor motor)
        {
            if (motor is null)
                return BadRequest();

            await motorService.UpdateAsync(motor);
            return Ok(motor);
        }

        /// <summary>
        /// Delete a Motor from database by selected Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Deleted successfully</response>
        /// <response code="400">Error while deleting object from database</response>
        [HttpDelete("{id}", Name = "DeleteMotor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteMotor(int id)
        {
            var motor = await motorService.GetByIdAsync(id);
            if (motor is null)
                return BadRequest();
            await motorService.DeleteAsync(id);
            return Ok();
        }
    }
}
