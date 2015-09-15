using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace Weather.Service.Core.Adapters
{
    public class RestClientAdapter : IRestClientAdapter
    {
        private const int Timeout = 15000;

        public RestClientResponse Get(string url, string resource, IDictionary<string, string> urlSegements)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(url),
                Timeout = Timeout
            };

            var request = new RestRequest
            {
                Resource = resource,
                Method = Method.GET
            };

            foreach (var urlSegment in urlSegements)
                request.AddUrlSegment(urlSegment.Key, urlSegment.Value);

            var result = client.Execute(request);
            var restClientResponse = new RestClientResponse
            {
                Content = result.Content,
                ResponseStatus =
                    result.StatusCode == HttpStatusCode.OK ? RestResponseStatus.Success : RestResponseStatus.Failure
            };

            return restClientResponse;
        }
    }
}
