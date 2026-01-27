using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CalculatorController> _logger;
        private const int MAX_FACTORIAL = 20; // Prevent DoS via stack overflow/recursion & int64 overflow

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

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
            catch (OverflowException ex)
            {
                _logger.LogWarning(ex, "Integer overflow on add request");
                return BadRequest("Overflow occurred: numbers too large.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in Add");
                return StatusCode(500, "An error occurred");
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
            catch (OverflowException ex)
            {
                _logger.LogWarning(ex, "Integer overflow on sub request");
                return BadRequest("Overflow occurred: numbers too large.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in Sub");
                return StatusCode(500, "An error occurred");
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
                    if (result > int.MaxValue || result < int.MinValue)
                        throw new OverflowException();
                    return Ok(result);
                }
            }
            catch (OverflowException ex)
            {
                _logger.LogWarning(ex, "Integer overflow on multiply request");
                return BadRequest("Overflow occurred: numbers too large.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in Multiply");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num2 == 0)
            {
                return BadRequest("Division by zero is not allowed.");
            }
            try
            {
                return Ok((double)num1 / num2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in Divide");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0)
                return BadRequest("Factorial is not defined for negative numbers.");
            if (n > MAX_FACTORIAL)
                return BadRequest($"n is too large. Maximum allowed is {MAX_FACTORIAL} to avoid overflows.");
            try
            {
                return Ok(FactorialIter(n));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in Factorial");
                return StatusCode(500, "An error occurred");
            }
        }

        private static long FactorialIter(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result = checked(result * i);
            }
            return result;
        }
    }
}
