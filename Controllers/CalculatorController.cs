using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int MaxFactorialInput = 20; // Limit to prevent stack overflow or excessive CPU usage

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Calculation caused an overflow.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Calculation caused an overflow.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Calculation caused an overflow.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num2 == 0)
            {
                return BadRequest("Division by zero is not allowed.");
            }

            return Ok((double)num1 / num2); // Return result as double to handle precision
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("The input must be a non-negative integer.");
            if (n > MaxFactorialInput) return BadRequest($"Input is too large. Maximum allowed value is {MaxFactorialInput}.");

            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private BigInteger CalculateFactorial(int n)
        {
            BigInteger result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}