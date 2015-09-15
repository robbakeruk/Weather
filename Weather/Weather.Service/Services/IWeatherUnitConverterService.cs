using Weather.Service.Models;

namespace Weather.Service.Services
{
    public interface IWeatherUnitConverterService
    {
        void Convert(WeatherServiceResponse weatherServiceResponse);
    }
}
