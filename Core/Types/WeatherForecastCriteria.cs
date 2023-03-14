
namespace Types
{
    public class WeatherForecastCriteria
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Days { get; set; }
        public string Supplier { get; set; } = string.Empty;
    }
}
