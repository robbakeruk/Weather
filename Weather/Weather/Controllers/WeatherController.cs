using System.Threading.Tasks;
using System.Web.Mvc;
using Weather.Models;
using Weather.Service.Services;

namespace Weather.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherAggregatorService _weatherAggregatorService;

        public WeatherController(IWeatherAggregatorService weatherAggregatorService)
        {
            _weatherAggregatorService = weatherAggregatorService;
        }

        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(WeatherModel model)
        {
            if (ModelState.IsValid)
            {
                var weatherResult = _weatherAggregatorService.GetAggregatedWeatherFromRegisteredServices(model.Location);

                model.Temperature = (model.TemperatureUnit == "Celsius") ? weatherResult.AverageTemperatureCelsius : weatherResult.AverageTemperatureFahrenheit;
                model.WindSpeed = (model.TemperatureUnit == "Mph") ? weatherResult.AverageWindSpeedMph : weatherResult.AverageWindSpeedKph;
            }

            return View(model);
        }
    }
}
