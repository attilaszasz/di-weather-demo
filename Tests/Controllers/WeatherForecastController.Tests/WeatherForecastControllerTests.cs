using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastController.Tests
{
    [TestClass]
    public class WeatherForecastControllerTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            //NOTE: we cannot test WeatherForecastService in isolation because the weather suppliers are tightly coupled to it
            // For example, we'd like to test if logging is done correctly
            var controller = new Api.Controllers.WeatherForecastController();
            var result = await controller.Get(
                new Types.WeatherForecastCriteria
                {
                    Longitude = 46.542679,
                    Latitude = 24.557859,
                    Days = 1,
                    Supplier = "DummyWeatherSupplier"
                });
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}