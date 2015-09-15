namespace Weather.Service.Models
{
    public class WeatherServiceResponse
    {
        public string Location { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
        public double WindSpeedKph { get; set; }
        public double WindSpeedMph { get; set; }
    }
}
