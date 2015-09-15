using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Weather.Service.Models;
using Weather.Service.Repository;

namespace Weather.Service.Services
{
    public class WeatherAggregatorService : IWeatherAggregatorService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IWeatherServiceStore _weatherServiceStore;
        private readonly IWeatherUnitConverterService _weatherUnitConverterService;

        public WeatherAggregatorService(IWeatherRepository weatherRepository, IWeatherServiceStore weatherServiceStore, IWeatherUnitConverterService weatherUnitConverterService)
        {
            _weatherRepository = weatherRepository;
            _weatherServiceStore = weatherServiceStore;
            _weatherUnitConverterService = weatherUnitConverterService;
        }
 
        public AggregatedWeatherResponse GetAggregatedWeatherFromRegisteredServices(string location)
        {
            if (string.IsNullOrEmpty(location))
                return null;

            var weatherServiceResults = GetWeatherFromAllRegisteredServices(location);

            ConvertWeatherForMissingValues(weatherServiceResults);

            var averagesToReturn = CalculateAverages(weatherServiceResults.ToList());

            if (averagesToReturn != null)
                averagesToReturn.Location = location;

            return averagesToReturn;
        }

        private void ConvertWeatherForMissingValues(IEnumerable<WeatherServiceResponse> weatherServiceResults)
        {
            foreach (var result  in weatherServiceResults)
                _weatherUnitConverterService.Convert(result);
        }

        private IList<WeatherServiceResponse> GetWeatherFromAllRegisteredServices(string location)
        {
            return _weatherServiceStore.Services.Select(weatherService => 
                GetWeatherFromRegisteredService(location, weatherService)).Where(response => response != null)
                .ToList();
        }

        private WeatherServiceResponse GetWeatherFromRegisteredService(string location, WeatherServiceDetail weatherService)
        {
            try
            {
                return _weatherRepository.Get(weatherService, location);
            }
            catch (WeatherServiceException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        private static AggregatedWeatherResponse CalculateAverages(IList<WeatherServiceResponse> weatherServiceResponses)
        {
            AggregatedWeatherResponse aggregatedResponse = null;

            if (weatherServiceResponses.Count > 0)
            {
                aggregatedResponse = new AggregatedWeatherResponse
                {
                    AverageTemperatureCelsius = weatherServiceResponses.Average(a => a.TemperatureCelsius),
                    AverageTemperatureFahrenheit = weatherServiceResponses.Average(a => a.TemperatureFahrenheit),
                    AverageWindSpeedKph = weatherServiceResponses.Average(a => a.WindSpeedKph),
                    AverageWindSpeedMph = weatherServiceResponses.Average(a => a.WindSpeedMph)
                };
            }

            return aggregatedResponse;
        }
    }
}
