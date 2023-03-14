using Interfaces.Acces;
using Interfaces.Service;
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
    public  class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherSupplier _dummyWeatherSupplier;
        private readonly IWeatherSupplier _openWeatherAPISupplier;

        public WeatherForecastService(IWeatherSupplier dummyWeatherSupplier, IWeatherSupplier openWeatherAPISupplier)
        {
            _dummyWeatherSupplier = dummyWeatherSupplier;
            _openWeatherAPISupplier = openWeatherAPISupplier;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(WeatherForecastCriteria criteria)
        {
            IWeatherSupplier? weather = null;
            //NOTE: using magic strings to remove dependency on specific classes. Not a better solution!
            if (criteria.Supplier == "DummyWeatherSupplier") weather = _dummyWeatherSupplier;
            if (criteria.Supplier == "OpenWeatherAPISupplier") weather = _openWeatherAPISupplier;
            if (weather == null) throw new NullReferenceException("Please specify an existing weather supplier!");
            return await weather.GetWeatherForecast(criteria);
        }
    }
}
