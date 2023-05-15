using NUnit.Framework;

namespace IntegrationTests
{
    // TODO: Add more tests

    [TestFixture]
    public class ShapesServiceTests : TestBase
    {
        [Test]
        public async Task GetSquares_Should_Get_Squares_Correctly()
        {
            await ClearCoordinates();
            await SetUp(GetCoordinatesMockData());

            var result = await ShapesService.GetSquares();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));

            result.ForEach(x =>
            {
                Assert.That(x.Count, Is.EqualTo(4));
            });
        }
    }
}
