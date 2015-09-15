using FluentAssertions;
using Weather.Service.Models;
using Weather.Service.Services;
using Xunit;

namespace Weather.Test.Unit
{
    public class WeatherServiceStoreTest
    {
        [Fact]
        public void ServicesInitializedSuccessfullyTest()
        {
            //Arrange
            var weatherServiceStore = new WeatherServiceStore();

            //Act
            var services = weatherServiceStore.Services;

            //Assert
            services.Should().BeEmpty("No services added");
        }

        [Fact]
        public void RegisterRegistersServiceSuccessfullyTest()
        {
            //Arrange
            var weatherServiceStore = new WeatherServiceStore();
            var weatherServiceDetail = new WeatherServiceDetail()
            {
                Resource = "Test",
                ServiceUrl = "Test2"
            };

            //Act
            weatherServiceStore.RegisterService(weatherServiceDetail);
            var services = weatherServiceStore.Services;

            //Assert
            services.Should().HaveCount(1, "1 service registered");
            services[0].ShouldBeEquivalentTo(weatherServiceDetail);
        }
    }
}
