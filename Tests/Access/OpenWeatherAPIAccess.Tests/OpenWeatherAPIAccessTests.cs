using OpenWeatherApiAccess;

namespace OpenWeatherAPIAccess.Tests
{
    [TestClass]
    public class OpenWeatherAPIAccessTests
    {
        [TestMethod]
        public async Task TestSingleResult()
        {
            var access = new OpenWeatherAPISupplier();
            var result = await access.GetWeatherForecast(46.542679, 24.557859, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}