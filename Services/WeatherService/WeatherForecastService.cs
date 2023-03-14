using OpenWeatherApiAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;
using WeatherAccess;

namespace WeatherService
{
    public  class WeatherForecastService
    {
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(double latitude, double longitude, int days, string supplier)
        {
            //Note: service is tightly coupled to both dummy and openweather supplier
            WeatherSupplierBase? weather = null;
            if (supplier == nameof(DummyWeatherSupplier)) weather = new DummyWeatherSupplier();
            if (supplier == nameof(OpenWeatherAPISupplier)) weather = new OpenWeatherAPISupplier();
            if (weather == null) throw new NullReferenceException("Please specify an existing weather supplier!");
            return await weather.GetWeatherForecast(latitude, longitude, days);
        }
    }
}
