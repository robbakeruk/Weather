using FluentAssertions;
using Weather.Service.Core.Adapters;
using Weather.Service.Models;
using Weather.Service.Repository;
using Weather.Service.Services;
using Xunit;
using Container = Weather.Test.Core.Container;

namespace Weather.Test.Integration
{
    public class WeatherAggregatorServiceTest
    {
        [Fact(Skip = "weather service api's need to be running")]
        public void GetWeatherTest()
        {
            //Arrange
            var container = new Container();
            var weatherServices = new WeatherServiceStore();
            weatherServices.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "http://localhost:18888/api",
                Resource = "Weather/{location}"
            });
            weatherServices.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "http://localhost:17855/api",
                Resource = "Weather/{location}"
            });

            container.Register<IWeatherAggregatorService, WeatherAggregatorService>();
            container.Register<IWeatherRepository, WeatherRepository>();
            container.Register<IWeatherUnitConverterService, WeatherUnitConverterService>();
            container.Register<IRestClientAdapter, RestClientAdapter>();
            container.Register<IJsonConvertAdapter, JsonConvertAdapter>();
            container.RegisterInstance<IWeatherServiceStore>(weatherServices);

            var weatherAggregatorService = container.Get<IWeatherAggregatorService>();

            const string location = "TestLocation";

            //Act
            var result = weatherAggregatorService.GetAggregatedWeatherFromRegisteredServices(location);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
