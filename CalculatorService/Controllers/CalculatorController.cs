using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            // Validate input range to prevent overflow
            if (!ModelState.IsValid) return BadRequest("Invalid input.");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input numbers must be between -1000000 and 1000000.");

            try
            {
                checked // Ensure overflow checking
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred due to large/small values.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input.");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input numbers must be between -1000000 and 1000000.");

            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred due to large/small values.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input.");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input numbers must be between -1000000 and 1000000.");

            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    if (result > int.MaxValue || result < int.MinValue)
                        return BadRequest("Result overflow. Try smaller numbers.");
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred due to large/small values.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input.");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input numbers must be between -1000000 and 1000000.");
            if (num2 == 0)
                return BadRequest("Division by zero is not allowed.");

            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero is not allowed.");
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred due to large/small values.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        /// <summary>
        /// Validate input numbers to avoid possible int overflows and logic issues.
        /// </summary>
        private bool IsValidInput(int num)
        {
            return num >= -1000000 && num <= 1000000;
        }
    }
}
