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
            if (num1 < 0 || num2 < 0) return BadRequest("Inputs must be non-negative integers");
            if (num1 > int.MaxValue - num2) return BadRequest("Sum exceeds allowable range");
            
            try
            {
                return Ok(num1 + num2);
            }
            catch (Exception ex)
            {
                // Log the error (log mechanism implementation not shown)
                return StatusCode(500, "An internal error occurred: " + ex.Message);
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 < 0 || num2 < 0) return BadRequest("Inputs must be non-negative integers");

            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 < 0 || num2 < 0) return BadRequest("Inputs must be non-negative integers");
            if (num1 > 0 && num2 > int.MaxValue / num1) return BadRequest("Product exceeds allowable range");

            try
            {
                long result = (long)num1 * (long)num2;
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An internal error occurred: " + ex.Message);
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 < 0 || num2 <= 0) return BadRequest("Inputs must be non-negative and divisor must be greater than zero");
            
            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException dbzEx)
            {
                // Log the error
                return StatusCode(500, "Divide by zero error: " + dbzEx.Message);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An internal error occurred: " + ex.Message);
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Input must be a non-negative integer");

            try
            {
                long result = 1;
                for (int i = 1; i <= n; i++)
                {
                    if (result > long.MaxValue / i) return BadRequest("Factorial result exceeds allowable range");
                    result *= i;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An internal error occurred: " + ex.Message);
            }
        }
    }
}