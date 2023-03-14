using Microsoft.Extensions.Configuration;
using OpenWeatherApiAccess;

namespace OpenWeatherAPIAccess.Tests
{
    [TestClass]
    public class OpenWeatherAPIAccessTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            var access = new OpenWeatherAPISupplier(new ConfigurationBuilder().AddUserSecrets("5260b863-91bc-46e7-adfb-b225ff3f690b").Build());
            var result = await access.GetWeatherForecast(
                new Types.WeatherForecastCriteria
                {
                    Longitude = 46.542679,
                    Latitude = 24.557859,
                    Days = 1
                });
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}