using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics; // For BigInteger

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            // Input validation for range
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
            {
                return BadRequest("Invalid input range for numbers.");
            }

            try
            {
                // Prevent integer overflow
                long result = (long)num1 + (long)num2;
                if (result > int.MaxValue || result < int.MinValue)
                {
                    return BadRequest("Result exceeds int range.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log error securely (not exposing implementation details)
                // _logger.LogError(ex, "Add operation failed.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
            {
                return BadRequest("Invalid input range for numbers.");
            }
            long result = (long)num1 - (long)num2;
            if (result > int.MaxValue || result < int.MinValue)
            {
                return BadRequest("Result exceeds int range.");
            }
            return Ok(result);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
            {
                return BadRequest("Invalid input range for numbers.");
            }
            long result = 0;
            try
            {
                result = (long)num1 * (long)num2;
                // Prevent integer overflow (if more than long range)
                if (result > long.MaxValue || result < long.MinValue)
                    return BadRequest("Result exceeds long range.");
            }
            catch (Exception)
            {
                return BadRequest("Overflow or calculation error.");
            }
            return Ok(result);
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < int.MinValue || num1 > int.MaxValue || num2 < int.MinValue || num2 > int.MaxValue)
            {
                return BadRequest("Invalid input range for numbers.");
            }
            if (num2 == 0)
            {
                return BadRequest("Division by zero is not allowed.");
            }
            try
            {
                int result = num1 / num2;
                return Ok(result);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero exception.");
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred during division.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred during division.");
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            // Limit input to avoid stack overflow (infinite recursion)
            if (!ModelState.IsValid || n < 0 || n > 20) // 20! fits in long
                return BadRequest("Input must be between 0 and 20 for factorial.");

            try
            {
                long fact = 1;
                for (int i = 2; i <= n; i++)
                {
                    fact *= i;
                }
                return Ok(fact);
            }
            catch (Exception)
            {
                return BadRequest("Calculation error in factorial.");
            }
        }
    }
}
