using Interfaces.Acces;
using Microsoft.Extensions.Configuration;
using Moq;
using OpenWeatherApiAccess;
using Types;
using WeatherAccess;

namespace WeatherService.Tests
{
    [TestClass]
    public class WeatherServiceTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            var service = new WeatherForecastService(new DummyWeatherSupplier(), new OpenWeatherAPISupplier(new ConfigurationBuilder().AddUserSecrets("5260b863-91bc-46e7-adfb-b225ff3f690b").Build()));

            var results = await service.GetWeatherForecast(
                new Types.WeatherForecastCriteria 
                { 
                    Longitude = 46.542679, 
                    Latitude = 24.557859, 
                    Days = 1,
                    Supplier = "DummyWeatherSupplier"
                });
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());
        }

        [TestMethod]
        public async Task WeatherForecastServiceInIsolation() 
        { 
            var mockDummyWeatherSupplier = new Mock<IWeatherSupplier>();
            var mockOpenWeatherAPISupplier = new Mock<IWeatherSupplier>();
            var criteria = new WeatherForecastCriteria
            {
                Longitude = 46.542679,
                Latitude = 24.557859,
                Days = 1,
                Supplier = "DummyWeatherSupplier"
            };

            mockDummyWeatherSupplier.Setup(s => s.GetWeatherForecast(criteria)).Returns(Task.FromResult(
                Enumerable.Range(1, criteria.Days).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = string.Empty
                })));

            var service = new WeatherForecastService(mockDummyWeatherSupplier.Object, mockOpenWeatherAPISupplier.Object);
            var results = await service.GetWeatherForecast(criteria);

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());
            mockDummyWeatherSupplier.Verify(s => s.GetWeatherForecast(criteria), Times.Once());
        }
    }
}