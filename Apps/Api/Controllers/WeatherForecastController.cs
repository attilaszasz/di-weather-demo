using Microsoft.AspNetCore.Mvc;
using Types;
using WeatherService;
using Serilog;
using Serilog.Events;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    readonly Serilog.ILogger logger = new LoggerConfiguration().WriteTo.File("log.txt", LogEventLevel.Verbose, rollingInterval: RollingInterval.Day).CreateLogger();

    public WeatherForecastController()
    {
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(int days = 8)
    {
        try
        {
            //Note: controller is tightly coupled to WeatherForecastService.
            var service = new WeatherForecastService();
            var result = await service.GetWeatherForecast(days);
            logger.Information("Responded weather request {@params} with {@data}", days, result);
            return result;
        } 
        catch (Exception ex)
        {
            logger.Error(ex, "Error while getting weather info. Input {@params}", days);
            return Enumerable.Empty<WeatherForecast>();
        }
    }
}
