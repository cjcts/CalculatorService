using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");

            // Vulnerability: No protection against extremely large numbers that could cause
            // integer overflow or excessive resource consumption
            try
            {
                return Ok(num1 + num2);
            }
            catch
            {
                // Vulnerability: Generic error handling that might expose stack traces
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            // Vulnerability: No input validation for minimum/maximum values
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            // Vulnerability: No rate limiting - this expensive operation could be called repeatedly
            // Also no protection against integer overflow
            long result = (long)num1 * (long)num2;
            return Ok(result);
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            // Vulnerability: No timeout protection for the operation
            // Also division by zero check is not comprehensive (what if num2 becomes zero after casting?)
            return Ok(num1 / num2);
        }

        // New vulnerable endpoint demonstrating Unrestricted Resource Consumption
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            // Vulnerability: No maximum value check - this recursive implementation
            // could cause stack overflow or excessive CPU usage with large inputs
            if (n <= 1)
                return Ok(1);
            
            return Ok(n * Factorial(n - 1).Value);
        }
    }
}
