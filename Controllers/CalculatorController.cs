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
        private const int MaxFactorialInput = 20; // Prevent stack overflow

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Both numbers are required and cannot be zero.");
            
            try
            {
                return Ok(num1 + num2);
            }
            catch (Exception ex)
            {
                // Log error details securely (not implemented here)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Both numbers are required and cannot be zero.");
            
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Both numbers are required and cannot be zero.");
            
            try
            {
                long result = (long)num1 * (long)num2;
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log error details securely (not implemented here)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num2 == 0) return BadRequest("Division by zero is not allowed.");

            try
            {
                return Ok((double)num1 / num2);
            }
            catch (Exception ex)
            {
                // Log error details securely (not implemented here)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Input must be a non-negative integer.");
            if (n > MaxFactorialInput) return BadRequest("Input is too large.");

            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (Exception ex)
            {
                // Log error details securely (not implemented here)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        private long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
