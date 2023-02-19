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
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int days)
        {
            //Note: service is tightly coupled to dummy supplier
            var supplier = new DummyWeatherSupplier();
            return await supplier.GetWeatherForecast(days);
        }
    }
}
