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
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            try
            {
                checked // Prevent integer overflow
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow occurred.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked // Prevent integer overflow/underflow
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow or underflow occurred.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked // Prevent integer overflow
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow occurred.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            try
            {
                // Prevent divide by zero (already checked), integer overflow for int.MinValue / -1
                if (num2 == 0) return BadRequest("Cannot divide by zero.");
                if (num1 == int.MinValue && num2 == -1)
                    return BadRequest("Integer overflow.");

                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero.");
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow occurred.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
