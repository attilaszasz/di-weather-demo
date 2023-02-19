namespace WeatherAccess.Tests
{
    [TestClass]
    public class WeatherAccessTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            var access = new DummyWeatherSupplier();
            var result = await access.GetWeatherForecast(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}