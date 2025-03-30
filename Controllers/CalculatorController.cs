using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, "^\\d+$");
        }

        [HttpGet("add")]
        public IActionResult Add(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                int n1 = int.Parse(num1);
                int n2 = int.Parse(num2);

                checked
                {
                    return Ok(n1 + n2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("An error occurred while performing the addition.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                int n1 = int.Parse(num1);
                int n2 = int.Parse(num2);

                checked
                {
                    return Ok(n1 - n2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("An error occurred while performing the subtraction.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                int n1 = int.Parse(num1);
                int n2 = int.Parse(num2);

                checked
                {
                    long result = n1 * n2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("An error occurred while performing the multiplication.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                int n1 = int.Parse(num1);
                int n2 = int.Parse(num2);
                if (n2 == 0) return BadRequest("Division by zero is not allowed.");

                return Ok(n1 / n2);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while performing the division.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(string n)
        {
            if (!IsNumeric(n) || long.Parse(n) < 0) return BadRequest("Invalid input. Only positive numeric values are allowed.");

            try
            {
                int num = int.Parse(n);
                if (num > 20)
                {
                    return BadRequest("Input too large. Please try a smaller number.");
                }

                long result = 1;
                for (int i = 1; i <= num; i++)
                {
                    result *= i;
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while calculating the factorial.");
            }
        }
    }
}
