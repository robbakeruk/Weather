namespace Weather.Service.Models
{
    public class AggregatedWeatherResponse
    {
        public string Location { get; set; }
        public double AverageTemperatureCelsius { get; set; }
        public double AverageTemperatureFahrenheit { get; set; }
        public double AverageWindSpeedKph { get; set; }
        public double AverageWindSpeedMph { get; set; }
    }
}
