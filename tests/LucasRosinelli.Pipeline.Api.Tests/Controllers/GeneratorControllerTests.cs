using LucasRosinelli.Pipeline.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LucasRosinelli.Pipeline.Api.Tests.Controllers
{
    public class GeneratorControllerTests
    {
        private readonly GeneratorController _controller;

        public GeneratorControllerTests()
        {
            _controller = new GeneratorController();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void GeneratePassword_WithInvalidLength_ShouldReturnBadRequest(int length)
        {
            // Arrange

            // Act
            IActionResult result = _controller.GeneratePassword(length);

            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The length must be greater than zero.", objectResult.Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(500)]
        [InlineData(8000)]
        public void GeneratePassword_WithValidLength_ShouldSucceed(int length)
        {
            // Arrange
            bool HasOnlyValidCharacters(string password)
            {
                const string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%&+-*.,@?=[]()";

                foreach (var c in password)
                {
                    if (!validCharacters.Contains(c))
                    {
                        return false;
                    }
                }

                return true;
            }

            // Act
            IActionResult result = _controller.GeneratePassword(length);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var generated = Assert.IsType<string>(objectResult.Value);
            Assert.Equal(length, generated.Length);
            Assert.True(HasOnlyValidCharacters(generated));
        }
    }
}
