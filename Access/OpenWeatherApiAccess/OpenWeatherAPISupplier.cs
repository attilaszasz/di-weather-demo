using Interfaces.Acces;
using Microsoft.Extensions.Configuration;
using OpenWeatherMap.Standard;
using OpenWeatherMap.Standard.Enums;
using System;
using Types;

namespace OpenWeatherApiAccess
{
    public class OpenWeatherAPISupplier : IWeatherSupplier
    {
        private string _apiToken;

        //NOTE: because the OpenWeatherMap library does not follow intarface based programming model,
        // there is a strong dependency on the Current class
        private Current _supplier;

        public OpenWeatherAPISupplier(IConfiguration configuration)
        {
            _apiToken = configuration.GetSection("OpenWeatherAPIToken").Value ?? string.Empty;

            if (string.IsNullOrWhiteSpace(_apiToken)) throw new NullReferenceException("Please set the OpenWeatherAPIToken user secret");

            _supplier = new Current(_apiToken, WeatherUnits.Metric);
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(WeatherForecastCriteria criteria)
        {
            var results = await _supplier.GetForecastDataByCoordinatesAsync(criteria.Latitude, criteria.Longitude);

            return results
                .WeatherData
                .Where(w => w.AcquisitionDateTime > DateTime.Today.AddHours(23).AddMinutes(59) && w.AcquisitionDateTime <= DateTime.Today.AddDays(criteria.Days))
                .ToList()
                .ConvertAll(w => new WeatherForecast 
                    {
                        Date = w.AcquisitionDateTime,
                        TemperatureC = (int)w.WeatherDayInfo.Temperature,
                        Summary = w.Weathers?.FirstOrDefault()?.Description ?? string.Empty            
                    });
        }
    }
}