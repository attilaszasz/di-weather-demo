
namespace Types
{
    public abstract class WeatherSupplierBase
    {
        public virtual async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(double latitude, double longitude, int days) { return await Task.FromResult(Enumerable.Empty<WeatherForecast>()); }
    }
}
