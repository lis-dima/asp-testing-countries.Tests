using asp_testing_countries.Controllers;
using asp_testing_countries.Services.Country;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace asp_testing_countries.Tests
{
    public class CreateCountryTest
    {
        private Mock<ICountryRepository> mockCountryRepo;
        private CountryController controller;

        public CreateCountryTest()
        {
            mockCountryRepo = new Mock<ICountryRepository>();
            controller = new CountryController(mockCountryRepo.Object);
        }

        [Fact]
        public async Task Should_Throw_Exception_For_Null_Request()
        {
            //Arrange
            CreateCountryRequest request = null;

            //Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(
                async () => controller.CreateCountry(request)
            );

            //Assert
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_If_Country_Already_Exist()
        {
            //Arrange
            CreateCountryRequest request = new() { Name = "UK" };
            mockCountryRepo.Setup(r => r.GetAll()).Returns(new List<Country>() {
                new() { Name = "UK" }
            });

            //Act
            var ex = await Assert.ThrowsAsync<Exception>(
                async () => controller.CreateCountry(request)
            );

            //Assert
            Assert.Contains("This country already exist", ex.Message);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(Exception))]
        public async Task Should_Throw_Exception_If_CountryName_Not_Proper(string? notSutableName, Type errType)
        {
            //Arrange
            CreateCountryRequest request = new() { Name = notSutableName };

            //Act + Assert
            var ex = await Assert.ThrowsAsync(
                errType,
                async () => controller.CreateCountry(request)
            );

        }
    }
}