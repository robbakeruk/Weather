using Weather.Service.Models;

namespace Weather.Service.Repository
{
    public interface IWeatherRepository
    {
        WeatherServiceResponse Get(WeatherServiceDetail weatherServiceDetail, string location);
    }
}
