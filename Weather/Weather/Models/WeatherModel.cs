using System.ComponentModel.DataAnnotations;

namespace Weather.Models
{
    public class WeatherModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Location:")]
        public string Location { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Wind Speed Unit:")]
        public string WindSpeedUnit { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Temperature Unit:")]
        public string TemperatureUnit { get; set; }

        [DataType(DataType.Text)]
        public double Temperature { get; set; }

        [DataType(DataType.Text)]
        public double WindSpeed { get; set; }
    }
}