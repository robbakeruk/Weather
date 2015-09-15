using System;

namespace Weather.Service
{
    public class WeatherServiceException : Exception
    {
        public WeatherServiceException(string message) : base(message)
        {
        }
    }
}
