using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementService<Measurement> measurementService;
        private readonly ILogger<MeasurementsController> logger;

        public MeasurementsController(IMeasurementService<Measurement> service, ILogger<MeasurementsController> Logger)
        {
            measurementService = service;
            logger = Logger;
        }

        /// <summary>
        /// Get all measurements from database
        /// </summary>
        /// <returns>IEnumerable collection</returns>
        /// <response code="200">Returns collection of Measurements</response>
        /// <response code="404">Database has no items</response>
        [HttpGet(Name = "GetMeasurements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurements()
        {
            var measurements = await measurementService.GetMeasurementsAsync();
            if (measurements is null)
                return NotFound();

            return Ok(measurements.ToList());
        }
    }
}
