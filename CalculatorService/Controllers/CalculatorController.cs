using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            // Validate against overflow/underflow
            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Input values are too large/small and caused an overflow.");
            }
            catch (Exception)
            {
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
            catch (OverflowException)
            {
                return BadRequest("Input values are too large/small and caused an overflow.");
            }
            catch (Exception)
            {
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
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Input values are too large/small and caused an overflow.");
            }
            catch (Exception)
            {
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
                checked
                {
                    return Ok(num1 / num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Input values are too large/small and caused an overflow.");
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero is not allowed.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
