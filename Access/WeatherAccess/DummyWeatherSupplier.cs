using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;

namespace WeatherAccess
{
    public class DummyWeatherSupplier : WeatherSupplierBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(double latitude, double longitude, int days)
        {
            return await Task.FromResult(
                Enumerable.Range(1, days).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }));
        }
    }
}
