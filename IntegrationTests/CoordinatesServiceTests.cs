using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace IntegrationTests
{
    // TODO: Use Mapper

    [TestFixture]
    public class CoordinatesServiceTests : TestBase
    {
        [Test]
        public async Task GetCoordinates_Should_Get_All_Coordinates()
        {
            var mockData = GetCoordinatesMockData();
            await SetUp(mockData);

            var coordinates = await CoordinatesService.GetCoordinates();

            Assert.That(coordinates, Is.Not.Empty);
            Assert.That(coordinates.Count, Is.EqualTo(mockData.Count));
        }

        [Test]
        public async Task SaveCoordinate_Should_Save_Coordinate()
        {
            var coordinateDto = new CoordinateDto
            {
                XCoord = 555,
                YCoord = 555
            };

            var id = (await CoordinatesService.SaveCoordinate(coordinateDto)).Id;

            var dbCoordinate = await DataContext.Coordinates.FindAsync(id);

            Assert.That(dbCoordinate, Is.Not.Null);
        }

        [Test]
        public async Task SaveCoordinates_Should_Save_Coordinates()
        {
            var coordinates = await CoordinatesService.SaveCoordinates(
                GetCoordinatesDtoMockData());

            var coordinateIds = coordinates.Select(x => x.Id).ToList();

            var dbCoordinates = await DataContext.Coordinates.Where(x => 
                coordinateIds.Contains(x.Id)).ToListAsync();

            Assert.That(dbCoordinates, Is.Not.Empty);
            Assert.That(dbCoordinates.Count, Is.EqualTo(coordinateIds.Count));
        }

        [Test]
        public async Task DeleteCoordinate_Should_Delete_Coordinate()
        {
            var mockData = GetCoordinatesMockData()[0];

            await SetUp(new List<Coordinate> { mockData });

            var id = mockData.Id;
            var deleteResult = await CoordinatesService.DeleteCoordinate(id);
            
            Assert.That(deleteResult, Is.True);

            var nullDbCoordinate = await DataContext.Coordinates.FindAsync(id);

            Assert.That(nullDbCoordinate, Is.Null);
        }

        [Test]
        public async Task DeleteCoordinates_Should_Delete_All_Coordinates()
        {
            var mockData = GetCoordinatesMockData();
            await SetUp(mockData);

            var ids = mockData.Select(x => x.Id).ToList();

            var deleteResult = await CoordinatesService.DeleteCoordinates();

            Assert.That(deleteResult, Is.True);

            var nullDbCoordinates = await DataContext.Coordinates.Where(x => 
                ids.Contains(x.Id)).ToListAsync();

            Assert.That(nullDbCoordinates, Is.Empty);
        }
    }
}