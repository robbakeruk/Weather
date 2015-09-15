using FluentAssertions;
using Rhino.Mocks;
using Weather.Service.Core.Adapters;
using Weather.Service.Models;
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

        [Theory]
        [InlineData(8.0, 10.0, 12.0, 7.5)]
        public void GetWeatherAverageWindSpeedValuesAreCorrectTest(double kph, double mph, double averageKph, double averageMph)
        {
            //Arrange
            const string location = "TestLocation";
            var container = new Container();
            var stubWeatherRespository = MockRepository.GenerateStub<IWeatherRepository>();
            var servicesStore = new WeatherServiceStore();

            container.RegisterInstance(stubWeatherRespository);
            container.RegisterInstance<IWeatherServiceStore>(servicesStore);
            container.Register<IJsonConvertAdapter, JsonConvertAdapter>();
            container.Register<IRestClientAdapter, RestClientAdapter>();
            container.Register<IWeatherUnitConverterService, WeatherUnitConverterService>();
            container.Register<IWeatherAggregatorService, WeatherAggregatorService>();

            servicesStore.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "Test1",
                Resource = "Weather"
            });

            servicesStore.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "Test2",
                Resource = "Weather"
            });

            stubWeatherRespository.Stub(x => x.Get(Arg<WeatherServiceDetail>.Is.Equal((servicesStore.Services[0])), Arg<string>.Is.Anything))
                .Return(new WeatherServiceResponse()
                {
                    WindSpeedKph = kph,
                    Location = location
                });

            stubWeatherRespository.Stub(x => x.Get(Arg<WeatherServiceDetail>.Is.Equal((servicesStore.Services[1])), Arg<string>.Is.Anything))
                .Return(new WeatherServiceResponse()
                {
                    WindSpeedMph = mph,
                    Location = location
                });


            var weatherAggregatorService = container.Get<IWeatherAggregatorService>();

            //Act
            var result = weatherAggregatorService.GetAggregatedWeatherFromRegisteredServices(location);

            //Assert
            result.AverageWindSpeedKph.Should().Be(averageKph);
            result.AverageWindSpeedMph.Should().Be(averageMph);
        }

        [Theory]
        [InlineData(10.0, 68.0, 15.0, 59.0)]
        public void GetWeatherAverageTemperatureValuesAreCorrectTest(double celsius, double fahrenheit, double averageCelsius, double averageFahrenheit)
        {
            //Arrange
            const string location = "TestLocation";
            var container = new Container();
            var stubWeatherRespository = MockRepository.GenerateStub<IWeatherRepository>();
            var servicesStore = new WeatherServiceStore();

            container.RegisterInstance(stubWeatherRespository);
            container.RegisterInstance<IWeatherServiceStore>(servicesStore);
            container.Register<IJsonConvertAdapter, JsonConvertAdapter>();
            container.Register<IRestClientAdapter, RestClientAdapter>();
            container.Register<IWeatherUnitConverterService, WeatherUnitConverterService>();
            container.Register<IWeatherAggregatorService, WeatherAggregatorService>();

            servicesStore.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "Test1",
                Resource = "Weather"
            });

            servicesStore.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "Test2",
                Resource = "Weather"
            });

            stubWeatherRespository.Stub(x => x.Get(Arg<WeatherServiceDetail>.Is.Equal((servicesStore.Services[0])), Arg<string>.Is.Anything))
                .Return(new WeatherServiceResponse()
                {
                    TemperatureCelsius = celsius,
                    Location = location
                });

            stubWeatherRespository.Stub(x => x.Get(Arg<WeatherServiceDetail>.Is.Equal((servicesStore.Services[1])), Arg<string>.Is.Anything))
                .Return(new WeatherServiceResponse()
                {
                    TemperatureFahrenheit = fahrenheit,
                    Location = location
                });


            var weatherAggregatorService = container.Get<IWeatherAggregatorService>();

            //Act
            var result = weatherAggregatorService.GetAggregatedWeatherFromRegisteredServices(location);

            //Assert
            result.AverageTemperatureCelsius.Should().Be(averageCelsius);
            result.AverageTemperatureFahrenheit.Should().Be(averageFahrenheit);
        }
    }
}
