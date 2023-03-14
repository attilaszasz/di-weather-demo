using Microsoft.AspNetCore.Mvc;
using Types;
using WeatherService;
using Serilog;
using Serilog.Events;
using Interfaces.Service;
using WeatherAccess;
using OpenWeatherApiAccess;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly Serilog.ILogger _logger = new LoggerConfiguration().WriteTo.File("log.txt", LogEventLevel.Verbose, rollingInterval: RollingInterval.Day).CreateLogger();
    private readonly IWeatherForecastService _weatherForecastService;

    //Note: without dependency injection, controllers cannot have parameters
    public WeatherForecastController()
    {
        //Note: controller is tightly coupled to WeatherForecastService and suppliers
        _weatherForecastService = new WeatherForecastService(
                                    dummyWeatherSupplier: new DummyWeatherSupplier(), 
                                    openWeatherAPISupplier: new OpenWeatherAPISupplier(new ConfigurationBuilder().AddUserSecrets("5260b863-91bc-46e7-adfb-b225ff3f690b").Build()));
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(WeatherForecastCriteria criteria)
    {
        try
        {
            var result = await _weatherForecastService.GetWeatherForecast(criteria);
            _logger.Information("Responded weather request {@criteria} with {@data}", criteria, result);
            return result;
        } 
        catch (Exception ex)
        {
            _logger.Error(ex, "Error while getting weather info. Input {@criteria}", criteria);
            return Enumerable.Empty<WeatherForecast>();
        }
    }
}
