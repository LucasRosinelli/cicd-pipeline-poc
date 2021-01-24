using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace LucasRosinelli.Pipeline.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private const int MinPasswordLength = 1;
        private const int MaxPasswordLength = 300;

        /// <summary>
        /// Generate a password with the given length.
        /// </summary>
        /// <param name="length">The password length.</param>
        /// <returns>The password.</returns>
        [HttpGet("password/{length:min(1):max(300)}")]
        public IActionResult GeneratePassword(int length = 16)
        {
            if (length < MinPasswordLength || length > MaxPasswordLength)
            {
                return BadRequest($"The length must be greater than or equal to {MinPasswordLength} and less than or equal to {MaxPasswordLength}.");
            }

            const string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%&+-*.,@?=[]()";
            var random = new Random();
            var password = new StringBuilder();
            while (password.Length < length)
            {
                password.Append(validCharacters[random.Next(0, validCharacters.Length)]);
            }

            return Ok(password.ToString());
        }
    }
}
