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
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (!ValidateNums(num1, num2, out IActionResult errorResult)) return errorResult;

            try
            {
                checked 
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Addition results in overflow");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (!ValidateNums(num1, num2, out IActionResult errorResult)) return errorResult;
            try
            {
                checked 
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Subtraction results in overflow");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (!ValidateNums(num1, num2, out IActionResult errorResult)) return errorResult;
            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    if (result > int.MaxValue || result < int.MinValue)
                        return BadRequest("Multiplication results in overflow");
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Multiplication results in overflow");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (!ValidateNums(num1, num2, out IActionResult errorResult)) return errorResult;
            if (num2 == 0)
            {
                return BadRequest("Divide by zero is not allowed");
            }
            try
            {
                checked
                {
                    return Ok(num1 / num2);
                }
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Divide by zero is not allowed");
            }
            catch (OverflowException)
            {
                return BadRequest("Division results in overflow");
            }
        }

        private bool ValidateNums(int num1, int num2, out IActionResult errorResult)
        {
            // Example validation for bounds. Could be more strict as needed for app context
            if (num1 > int.MaxValue || num1 < int.MinValue)
            {
                errorResult = BadRequest("Num1 out of bounds");
                return false;
            }
            if (num2 > int.MaxValue || num2 < int.MinValue)
            {
                errorResult = BadRequest("Num2 out of bounds");
                return false;
            }
            errorResult = null;
            return true;
        }
    }
}
