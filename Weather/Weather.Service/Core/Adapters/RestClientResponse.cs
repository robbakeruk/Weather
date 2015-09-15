namespace Weather.Service.Core.Adapters
{
    public class RestClientResponse
    {
        public string Content { get; set; }
        public RestResponseStatus ResponseStatus { get; set; }
    }
     
    public enum RestResponseStatus
    {
        Success,
        Failure
    }
}
