using Application.Repositories;
using Application.Services;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace IntegrationTests
{
    public class TestBase
    {
        public readonly DataContext _dataContext;
        public readonly CoordinatesService _coordinatesService;
        public readonly ShapesService _shapesService;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            _dataContext = new DataContext(options);

            var coordinatesRepository = new CoordinatesRepository(_dataContext);
            _coordinatesService = new CoordinatesService(coordinatesRepository);
            _shapesService = new ShapesService(coordinatesRepository);
        }

        public async Task SetUp(List<Coordinate> coordinates)
        {
            _dataContext.Coordinates.AddRange(coordinates);
            await _dataContext.SaveChangesAsync();
        }

        public List<CoordinateDto> GetCoordinatesDtoMockData()
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


        public List<Coordinate> GetCoordinatesMockData()
        {
            return new List<Coordinate>
            {
                new()
                {
                    XCoord = -1,
                    YCoord = 2,
                },
                new()
                {
                    XCoord = -1,
                    YCoord = 4,
                },
                new()
                {
                    XCoord = 1,
                    YCoord = 4,
                },
                new()
                {
                    XCoord = 1,
                    YCoord = 2,
                },
                new()
                {
                    XCoord = -3,
                    YCoord = 3,
                },
                new()
                {
                    XCoord = 1,
                    YCoord = 0,
                },
                new()
                {
                    XCoord = 3,
                    YCoord = 2,
                },
                new()
                {
                    XCoord = 3,
                    YCoord = 0,
                },
                new()
                {
                    XCoord = 2,
                    YCoord = -2,
                }
            };
        }
    }
}
