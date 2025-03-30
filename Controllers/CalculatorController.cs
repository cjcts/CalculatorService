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

            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow error occurred while adding numbers");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow error occurred while subtracting numbers");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            try
            {
                checked
                {
                    long result = num1 * num2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow error occurred while multiplying numbers");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Division by zero is not allowed");

            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division operation failed");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0)
            {
                return BadRequest("Factorial is not defined for negative numbers");
            }
            if (n > 20)
            {
                return BadRequest("Factorial input too large and can cause overflow");
            }

            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            return Ok(result);
        }
    }
}
