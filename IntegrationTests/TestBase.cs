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
        public readonly DataContext DataContext;
        public readonly CoordinatesService CoordinatesService;
        public readonly ShapesService ShapesService;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            DataContext = new DataContext(options);

            var coordinatesRepository = new CoordinatesRepository(DataContext);
            CoordinatesService = new CoordinatesService(coordinatesRepository);
            ShapesService = new ShapesService(coordinatesRepository);
        }

        public async Task SetUp(List<Coordinate> coordinates)
        {
            DataContext.Coordinates.AddRange(coordinates);
            await DataContext.SaveChangesAsync();
        }

        public async Task ClearCoordinates()
        {
            var allCoordinates = await DataContext.Coordinates.ToListAsync();
            DataContext.Coordinates.RemoveRange(allCoordinates);
            await DataContext.SaveChangesAsync();
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
