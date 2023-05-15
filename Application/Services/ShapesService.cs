using Entities.Models;
using Application.Repositories;
using Application.Contracts;

namespace Application.Services
{
    public class ShapesService : IShapesService
    {
        private readonly CoordinatesRepository _coordinatesRepository;

        public ShapesService(CoordinatesRepository coordinatesRepository)
        {
            _coordinatesRepository = coordinatesRepository;
        }

        public async Task<List<List<Coordinate>>> GetSquares()
        {
            var coordinates = await _coordinatesRepository.GetCoordinatesAsync();

            var n = coordinates.Count;
            var squareList = new List<List<Coordinate>>();

            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    for (var k = j + 1; k < n; k++)
                    {
                        for (var l = k + 1; l < n; l++)
                        {
                            var isSquare = IsSquare(
                                coordinates[i],
                                coordinates[j],
                                coordinates[k],
                                coordinates[l]);

                            if (isSquare)
                            {
                                squareList.Add(
                                    new List<Coordinate>
                                    {
                                        coordinates[i],
                                        coordinates[j],
                                        coordinates[k],
                                        coordinates[l]
                                    });
                            }
                        }
                    }
                }
            }

            return squareList;
        }

        private bool IsSquare(Coordinate c1, Coordinate c2, Coordinate c3, Coordinate c4)
        {
            var dist2 = GetDistance(c1, c2);
            var dist3 = GetDistance(c1, c3);
            var dist4 = GetDistance(c1, c4);

            if (dist2 == dist3 && 2 * dist2 == dist4)
            {
                var dist = GetDistance(c2, c4);
                return (dist == GetDistance(c3, c4) && dist == dist2);
            }

            if (dist3 == dist4 && 2 * dist3 == dist2)
            {
                var dist = GetDistance(c2, c3);
                return (dist == GetDistance(c2, c4) && dist == dist3);
            }

            if (dist2 == dist4 && 2 * dist2 == dist3)
            {
                var dist = GetDistance(c2, c3);
                return (dist == GetDistance(c3, c4) && dist == dist2);
            }

            return false;
        }

        private int GetDistance(Coordinate c1, Coordinate c2)
        {
            return (c1.XCoord - c2.XCoord) * (c1.XCoord - c2.XCoord) + (c1.YCoord - c2.YCoord) * (c1.YCoord - c2.YCoord);
        }
    }
}
