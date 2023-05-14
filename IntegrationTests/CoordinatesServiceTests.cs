using Application.Repositories;
using Application.Services;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence;

namespace IntegrationTests
{
    [TestFixture]
    public class CoordinatesServiceTests
    {
        private readonly DataContext _dataContext;
        private readonly CoordinatesRepository _coordinatesRepository;
        private readonly CoordinatesService _coordinatesService;

        public CoordinatesServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            _dataContext = new DataContext(options);
            _coordinatesRepository = new CoordinatesRepository(_dataContext);
            _coordinatesService = new CoordinatesService(_coordinatesRepository);
        }

        [Test]
        public async Task GetCoordinates_Should_Get_All_Coordinates()
        {
            var mockData = GetCoordinatesMockData();
            SetUp(mockData);

            var coordinates = await _coordinatesService.GetCoordinates();

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

            var id = (await _coordinatesService.SaveCoordinate(coordinateDto)).Id;

            var dbCoordinate = await _dataContext.Coordinates.FindAsync(id);

            Assert.That(dbCoordinate, Is.Not.Null);
        }

        [Test]
        public async Task SaveCoordinates_Should_Save_Coordinates()
        {
            var coordinates = await _coordinatesService.SaveCoordinates(
                GetCoordinatesDtoMockData());

            var coordinateIds = coordinates.Select(x => x.Id).ToList();

            var dbCoordinates = await _dataContext.Coordinates.Where(x => 
                coordinateIds.Contains(x.Id)).ToListAsync();

            Assert.That(dbCoordinates, Is.Not.Empty);
            Assert.That(dbCoordinates.Count, Is.EqualTo(coordinateIds.Count));
        }

        [Test]
        public async Task DeleteCoordinate_Should_Delete_Coordinate()
        {
            var mockData = GetCoordinatesMockData()[0];

            SetUp(new List<Coordinate> { mockData });

            var id = mockData.Id;
            var deleteResult = await _coordinatesService.DeleteCoordinate(id);
            
            Assert.That(deleteResult, Is.True);

            var nullDbCoordinate = await _dataContext.Coordinates.FindAsync(id);

            Assert.That(nullDbCoordinate, Is.Null);
        }

        [Test]
        public async Task DeleteCoordinates_Should_Delete_All_Coordinates()
        {
            var mockData = GetCoordinatesMockData();
            SetUp(mockData);

            var ids = mockData.Select(x => x.Id).ToList();

            var deleteResult = await _coordinatesService.DeleteCoordinates();

            Assert.That(deleteResult, Is.True);

            var nullDbCoordinates = await _dataContext.Coordinates.Where(x => 
                ids.Contains(x.Id)).ToListAsync();

            Assert.That(nullDbCoordinates, Is.Empty);
        }

        private void SetUp(List<Coordinate> coordinates)
        {
            _dataContext.Coordinates.AddRange(coordinates);
            _dataContext.SaveChanges();
        }

        private List<CoordinateDto> GetCoordinatesDtoMockData()
        {
            return new List<CoordinateDto>
            {
                new()
                {
                    XCoord = 111,
                    YCoord = 111,
                },
                new()
                {
                    XCoord = 222,
                    YCoord = 222,
                },
                new()
                {
                    XCoord = 333,
                    YCoord = 333,
                },
                new()
                {
                    XCoord = 444,
                    YCoord = 444,
                }
            };
        }


        private List<Coordinate> GetCoordinatesMockData()
        {
            return new List<Coordinate>
            {
                new()
                {
                    XCoord = 111,
                    YCoord = 111,
                },
                new()
                {
                    XCoord = 222,
                    YCoord = 222,
                },
                new()
                {
                    XCoord = 333,
                    YCoord = 333,
                },
                new()
                {
                    XCoord = 444,
                    YCoord = 444,
                }
            };
        }
    }
}