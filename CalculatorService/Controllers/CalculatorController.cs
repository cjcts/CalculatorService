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
            try
            {
                // Allow 0 as valid input.
                long result = (long)num1 + (long)num2;
                if (result > int.MaxValue || result < int.MinValue)
                    return BadRequest("Addition result is out of range.");
                return Ok((int)result);
            }
            catch (Exception ex)
            {
                // Log exception (not exposing details to client)
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            try
            {
                long result = (long)num1 - (long)num2;
                if (result > int.MaxValue || result < int.MinValue)
                    return BadRequest("Subtraction result is out of range.");
                return Ok((int)result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            try
            {
                long result = (long)num1 * (long)num2;
                if (result > int.MaxValue || result < int.MinValue)
                    return BadRequest("Multiplication result is out of range.");
                return Ok((int)result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            try
            {
                if (num2 == 0)
                    return BadRequest("Division by zero is not allowed.");
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero is not allowed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
