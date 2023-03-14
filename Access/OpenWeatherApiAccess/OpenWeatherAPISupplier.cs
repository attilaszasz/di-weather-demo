using Microsoft.Extensions.Configuration;
using OpenWeatherMap.Standard;
using OpenWeatherMap.Standard.Enums;
using System;
using Types;

namespace OpenWeatherApiAccess
{
    public class OpenWeatherAPISupplier : WeatherSupplierBase
    {
        public override async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(double latitude, double longitude, int days)
        {
            //See https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows on how to set up local user secrets
            var config = new ConfigurationBuilder().AddUserSecrets("5260b863-91bc-46e7-adfb-b225ff3f690b").Build();

            string token = config.GetSection("OpenWeatherAPIToken").Value ?? string.Empty;

            if (string.IsNullOrWhiteSpace(token)) throw new NullReferenceException("Please set the OpenWeatherAPIToken user secret");

            var supplier = new Current(token, WeatherUnits.Metric);
            var results = await supplier.GetForecastDataByCoordinatesAsync(latitude, longitude);

            return results
                .WeatherData
                .Where(w => w.AcquisitionDateTime > DateTime.Today.AddHours(23).AddMinutes(59) && w.AcquisitionDateTime <= DateTime.Today.AddDays(days))
                .ToList()
                .ConvertAll(w => new Types.WeatherForecast 
                    {
                        Date = w.AcquisitionDateTime,
                        TemperatureC = (int)w.WeatherDayInfo.Temperature,
                        Summary = w.Weathers?.FirstOrDefault()?.Description ?? string.Empty            
                    });
        }
    }
}