namespace WeatherAccess.Tests
{
    [TestClass]
    public class WeatherAccessTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            var access = new DummyWeatherSupplier();
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