using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;

namespace Interfaces.Acces
{
    public interface IWeatherSupplier
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecast(WeatherForecastCriteria criteria);
    }
}
