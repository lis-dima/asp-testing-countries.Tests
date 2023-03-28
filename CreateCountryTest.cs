using asp_testing_countries.Controllers;
using asp_testing_countries.Services.Country;
using Moq;
using System.Net.Mail;

namespace asp_testing_countries.Tests
{
    public class CreateCountryTest
    {
        [Fact]
        public async Task Should_Throw_Exception_For_Null_Request()
        {
            //Arrange
            CreateCountryRequest request = null;
            Mock<ICountryRepository> mockCountryRepo = new Mock<ICountryRepository>();
            var controller = new CountryController(mockCountryRepo.Object);
            
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(
                async () => controller.CreateCountry(request)
            );

            //Assert
            Assert.Contains("Value cannot be null", ex.Message);
        }
    }
}