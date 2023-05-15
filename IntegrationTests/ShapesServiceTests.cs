using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class ShapesServiceTests : TestBase
    {
        [Test]
        public async Task GetSquares_Should_Get_Squares_CorrectlyS()
        {
            await SetUp(GetCoordinatesMockData());

            var result = await _shapesService.GetSquares();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));

            result.ForEach(x =>
            {
                Assert.That(x.Count, Is.EqualTo(4));
            });
        }
    }
}
