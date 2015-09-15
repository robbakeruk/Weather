using System.Collections.Generic;
using Weather.Service.Models;

namespace Weather.Service.Services
{
    public interface IWeatherServiceStore
    {
        IList<WeatherServiceDetail> Services { get; }
        void RegisterService(WeatherServiceDetail weatherServiceDetail);
    }
}