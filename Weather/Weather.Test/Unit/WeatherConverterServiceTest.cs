using FluentAssertions;
using Weather.Service.Models;
using Weather.Service.Services;
using Xunit;

namespace Weather.Test.Unit
{
    public class WeatherConverterServiceTest
    {
        private const double TemperatureCelsius = 15.0;
        private const double TemperatureFahrenheit = 59.0;
        private const double WindSpeedMph = 20.0;
        private const double WindSpeedKph = 32.2;


        [Fact]
        public void ConvertCelsiusTest()
        {
            //Arrange
            var weatherUnitConverterService = new WeatherUnitConverterService();
            var weatherServiceResponse = new WeatherServiceResponse()
            {
                TemperatureCelsius = TemperatureCelsius
            };

            //Act
            weatherUnitConverterService.Convert(weatherServiceResponse);

            //Assert
            weatherServiceResponse.TemperatureFahrenheit.ShouldBeEquivalentTo(TemperatureFahrenheit);
        }

        [Fact]
        public void ConvertFahrenheitTest()
        {
            //Arrange
            var weatherUnitConverterService = new WeatherUnitConverterService();
            var weatherServiceResponse = new WeatherServiceResponse()
            {
                TemperatureFahrenheit = TemperatureFahrenheit
            };

            //Act
            weatherUnitConverterService.Convert(weatherServiceResponse);

            //Assert
            weatherServiceResponse.TemperatureCelsius.ShouldBeEquivalentTo(TemperatureCelsius);
        }

        [Fact]
        public void ConvertMphTest()
        {
            //Arrange
            var weatherUnitConverterService = new WeatherUnitConverterService();
            var weatherServiceResponse = new WeatherServiceResponse()
            {
                WindSpeedMph = WindSpeedMph
            };

            //Act
            weatherUnitConverterService.Convert(weatherServiceResponse);

            //Assert
            weatherServiceResponse.WindSpeedKph.ShouldBeEquivalentTo(WindSpeedKph);
        }

        [Fact]
        public void ConvertKphTest()
        {
            //Arrange
            var weatherUnitConverterService = new WeatherUnitConverterService();
            var weatherServiceResponse = new WeatherServiceResponse()
            {
                WindSpeedKph = WindSpeedKph
            };

            //Act
            weatherUnitConverterService.Convert(weatherServiceResponse);

            //Assert
            weatherServiceResponse.WindSpeedMph.ShouldBeEquivalentTo(WindSpeedMph);
        }
    }
}
