using System.Collections.Generic;

namespace Weather.Service.Core.Adapters
{
    public interface IRestClientAdapter
    {
        RestClientResponse Get(string url, string resource, IDictionary<string, string> urlSegements);     
    }
}
