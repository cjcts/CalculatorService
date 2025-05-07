
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
        private const int MaxInputValue = 1000000; // Prevent potential integer overflow

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            if (Math.Abs((long)num1 + (long)num2) > MaxInputValue) return BadRequest("Addition result exceeds allowed limits");

            try
            {
                return Ok(num1 + num2);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (Math.Abs((long)num1 - (long)num2) > MaxInputValue) return BadRequest("Subtraction result exceeds allowed limits");

            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            
            try
            {
                long result = (long)num1 * (long)num2;
                if (Math.Abs(result) > MaxInputValue) return BadRequest("Multiplication result exceeds allowed limits");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            
            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Input must be a non-negative integer");

            try
            {
                return Ok(IterativeFactorial(n));
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        private long IterativeFactorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
                if (result > MaxInputValue) throw new OverflowException("Factorial result exceeds allowed limits");
            }
            return result;
        }
    }
}