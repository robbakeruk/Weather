using Weather.Service.Models;

namespace Weather.Service.Services
{
    public interface IWeatherAggregatorService
    {
        AggregatedWeatherResponse GetAggregatedWeatherFromRegisteredServices(string location);
    }
}
