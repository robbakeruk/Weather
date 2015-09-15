using System.Collections.Generic;
using Weather.Service.Core.Adapters;
using Weather.Service.Models;

namespace Weather.Service.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IRestClientAdapter _restClient;
        private readonly IJsonConvertAdapter _jsonConvertAdapter;

        public WeatherRepository(IRestClientAdapter restClient, IJsonConvertAdapter jsonConvertAdapter)
        {
            _restClient = restClient;
            _jsonConvertAdapter = jsonConvertAdapter;
        }

        public WeatherServiceResponse Get(WeatherServiceDetail weatherServiceDetail, string location)
        {
            var urlSegment = new Dictionary<string, string> {{"location", location}};

            var response = _restClient.Get(weatherServiceDetail.ServiceUrl, weatherServiceDetail.Resource, urlSegment);

            if (response.ResponseStatus == RestResponseStatus.Failure)
                throw new WeatherServiceException("Service failed");

            return _jsonConvertAdapter.DeserializeObject<WeatherServiceResponse>(response.Content);
        }
    }
}
