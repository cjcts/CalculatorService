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
                return BadRequest("Invalid input");

            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            try
            {
                checked // prevent overflow
                {
                    return Ok(num1 + num2);
                }
            }
            catch (System.OverflowException)
            {
                return BadRequest("Overflow detected in addition operation.");
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
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked // prevent overflow
                {
                    return Ok(num1 - num2);
                }
            }
            catch (System.OverflowException)
            {
                return BadRequest("Overflow detected in subtraction operation.");
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
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked // prevent overflow
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (System.OverflowException)
            {
                return BadRequest("Overflow detected in multiplication operation.");
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
                return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                if (num2 == 0)
                    return BadRequest("Division by zero is not allowed.");
                return Ok(num1 / num2);
            }
            catch (System.DivideByZeroException)
            {
                return BadRequest("Division by zero detected.");
            }
            catch (System.OverflowException)
            {
                return BadRequest("Overflow detected in division operation.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
