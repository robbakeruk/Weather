using FluentAssertions;
using Rhino.Mocks;
using Weather.Service.Repository;
using Weather.Service.Services;
using Xunit;
using Container = Weather.Test.Core.Container;

namespace Weather.Test.Unit
{
    public class WeatherAggregatorServiceTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetWeatherLocationIsEmptyOrNullShouldReturnNullTest(string location)
        {
            //Arrange
            var container = new Container();
            var stubWeatherRespository = MockRepository.GenerateStub<IWeatherRepository>();
            var stubWeatherServiceStore = MockRepository.GenerateStub<IWeatherServiceStore>();
            var stubWeatherUnitConverterService = MockRepository.GenerateStub<IWeatherUnitConverterService>();

            container.RegisterInstance(stubWeatherRespository);
            container.RegisterInstance(stubWeatherServiceStore);
            container.RegisterInstance(stubWeatherUnitConverterService);
            container.Register<IWeatherAggregatorService, WeatherAggregatorService>();

            var weatherAggregatorService = container.Get<IWeatherAggregatorService>();

            //Act
            var result = weatherAggregatorService.GetAggregatedWeatherFromRegisteredServices(location);

            //Assert
            result.Should().BeNull("location is empty of null");
        }
    }
}
