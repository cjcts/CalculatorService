
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add([FromQuery] int num1, [FromQuery] int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");

            try
            {
                return Ok(num1 + num2);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "Overflow error occurred while adding.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub([FromQuery] int num1, [FromQuery] int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");

            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply([FromQuery] int num1, [FromQuery] int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");

            try
            {
                BigInteger result = (BigInteger)num1 * (BigInteger)num2;
                return Ok(result);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "Overflow error occurred while multiplying.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide([FromQuery] int num1, [FromQuery] int num2)
        {
            if (num2 == 0)
                return BadRequest("Division by zero is not allowed.");
            
            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial([FromQuery] int n)
        {
            if (n < 0)
                return BadRequest("Factorial of a negative number is not defined.");

            try
            {
                BigInteger result = ComputeFactorial(n);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        private BigInteger ComputeFactorial(int n)
        {
            BigInteger result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
