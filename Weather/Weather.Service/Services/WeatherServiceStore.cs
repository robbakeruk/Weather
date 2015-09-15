using System.Collections.Generic;
using Weather.Service.Models;

namespace Weather.Service.Services
{
    public class WeatherServiceStore : IWeatherServiceStore
    {
        public IList<WeatherServiceDetail> Services { get; }

        public WeatherServiceStore()
        {
            Services = new List<WeatherServiceDetail>();
        }

        public void RegisterService(WeatherServiceDetail weatherServiceDetail)
        {
            Services.Add(weatherServiceDetail);
        }
    }
}
