namespace WeatherService.Tests
{
    [TestClass]
    public class WeatherServiceTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            var service = new WeatherForecastService();

            //NOTE: we cannot test WeatherForecastService in isolation because the weather suppliers are tightly coupled to it
            var results = await service.GetWeatherForecast(46.542679, 24.557859, 1, "DummyWeatherSupplier");
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());
        }
    }
}