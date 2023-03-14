using Types;

namespace Interfaces.Service
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecast(WeatherForecastCriteria criteria);
    }
}