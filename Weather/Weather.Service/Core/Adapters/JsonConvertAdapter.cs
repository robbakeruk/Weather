using Newtonsoft.Json;

namespace Weather.Service.Core.Adapters
{
    public class JsonConvertAdapter : IJsonConvertAdapter
    {
        public TType DeserializeObject<TType>(string jsonToConvert)
        {
            return JsonConvert.DeserializeObject<TType>(jsonToConvert);
        }
    }
}
