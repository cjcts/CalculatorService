using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        // Helper: Validate integer range if needed.
        private bool IsIntInSafeRange(long value) =>
            value <= int.MaxValue && value >= int.MinValue;

        [HttpGet("add")]
        public IActionResult Add([FromQuery, Required] int num1, [FromQuery, Required] int num2)
        {
            try
            {
                long result = (long)num1 + (long)num2;
                if (!IsIntInSafeRange(result)) return BadRequest("Result out of range");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub([FromQuery, Required] int num1, [FromQuery, Required] int num2)
        {
            try
            {
                long result = (long)num1 - (long)num2;
                if (!IsIntInSafeRange(result)) return BadRequest("Result out of range");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply([FromQuery, Required] int num1, [FromQuery, Required] int num2)
        {
            try
            {
                long result = (long)num1 * (long)num2;
                if (num1 == 0 || num2 == 0) return BadRequest("Num1 and Num2 are required and cannot be zero");
                if (!IsIntInSafeRange(result)) return BadRequest("Result out of range");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide([FromQuery, Required] int num1, [FromQuery, Required] int num2)
        {
            try
            {
                if (num2 == 0) return BadRequest("Division by zero is not allowed");
                long result = (long)num1 / (long)num2; // integer division
                if (!IsIntInSafeRange(result)) return BadRequest("Result out of range");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
