namespace Weather.Service.Core.Adapters
{
    public interface IJsonConvertAdapter
    {
        TType DeserializeObject<TType>(string jsonToConvert);
    }
}