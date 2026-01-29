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
            // Input validation to prevent overflow/underflow and DoS
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            try
            {
                checked // Ensure overflow is detected
                {
                    return Ok(num1 + num2);
                }
            }
            catch(System.OverflowException)
            {
                return BadRequest("Overflow occurred during addition");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch(System.OverflowException)
            {
                return BadRequest("Overflow occurred during subtraction");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch(System.OverflowException)
            {
                return BadRequest("Overflow occurred during multiplication");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                // Prevent divide by zero and handle overflows
                checked {
                    return Ok(num1 / num2);
                }
            }
            catch (System.DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero");
            }
            catch(System.OverflowException)
            {
                return BadRequest("Overflow occurred during division");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
