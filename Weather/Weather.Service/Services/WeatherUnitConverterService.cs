using System;
using Weather.Service.Models;

namespace Weather.Service.Services
{
    public class WeatherUnitConverterService : IWeatherUnitConverterService
    {
        private const int Digits = 1;

        public void Convert(WeatherServiceResponse weatherServiceResponse)
        {
            ConvertTemperature(weatherServiceResponse);
            ConvertWind(weatherServiceResponse);
        }

        private static void ConvertTemperature(WeatherServiceResponse weatherServiceResponse)
        {
            if (weatherServiceResponse.TemperatureCelsius > 0)
            {
                var tempFahrenheit =
                    UnitsNet.Temperature.FromDegreesCelsius(weatherServiceResponse.TemperatureCelsius).DegreesFahrenheit;
                weatherServiceResponse.TemperatureFahrenheit = Math.Round(tempFahrenheit, Digits);
            }
            else
            {
                var tempCelsius =
                    UnitsNet.Temperature.FromDegreesFahrenheit(weatherServiceResponse.TemperatureFahrenheit)
                        .DegreesCelsius;
                weatherServiceResponse.TemperatureCelsius = Math.Round(tempCelsius, Digits);
            }
        }

        private static void ConvertWind(WeatherServiceResponse weatherServiceResponse)
        {
            if (weatherServiceResponse.WindSpeedKph > 0)
            {
                var windMph = 
                    UnitsNet.Speed.FromKilometersPerHour(weatherServiceResponse.WindSpeedKph).MilesPerHour;
                weatherServiceResponse.WindSpeedMph = Math.Round(windMph, Digits);
            }
            else
            {
                var windKph =
                    UnitsNet.Speed.FromMilesPerHour(weatherServiceResponse.WindSpeedMph).KilometersPerHour;
                weatherServiceResponse.WindSpeedKph = Math.Round(windKph, Digits);
            }
        }
    }
}
