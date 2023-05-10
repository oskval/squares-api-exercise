using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using squares_api_exercise.Contracts;
using squares_api_exercise.Models;

namespace squares_api_exercise.Services
{
    public class SquaresService : ISquaresService
    {
        // TODO: move to database
        private List<Coordinate> _coordinateList = new();

        public List<Coordinate> Get()
        {
            if (!_coordinateList.Any())
            {
                throw new Exception("Please add a coordinates and try again.");
            }
            return _coordinateList;
        }

        public Coordinate Add(CoordinateDto coordinateDto)
        {
            var coordinate = new Coordinate
            {
                Id = Guid.NewGuid(),
                X = coordinateDto.X,
                Y = coordinateDto.Y,
            };

            _coordinateList.Add(coordinate);

            return coordinate;
        }

        public List<Coordinate> Post(List<CoordinateDto> coordinateDtoList)
        {
            var coordinateList = coordinateDtoList.Select(x =>
            {
                var id = Guid.NewGuid();

                var coordinate = new Coordinate
                {
                    Id = id,
                    X = x.X,
                    Y = x.Y,
                };

                return coordinate;
            }).ToList();

            _coordinateList = coordinateList;
            return coordinateList;
        }

        public bool Delete(string id)
        {
            if (Guid.TryParse(id, out var guidId))
            {
                throw new Exception($"The provided GUID is not valid: {id}");
            }

            var coordinate = _coordinateList.FirstOrDefault(x => x.Id == guidId);

            if (coordinate == null)
            {
                throw new Exception($"No Coordinate found by given id: {id}");
            }

            _coordinateList.Remove(coordinate); 
            return true;
        }

        public bool DeleteAll()
        {
            if (!_coordinateList.Any())
            {
                throw new Exception("Coordinate list is already empty.");
            }

            _coordinateList.Clear();
            return true;
        }

        public List<List<Coordinate>> GetSquares()
        {
            var n = _coordinateList.Count;
            var squareList = new List<List<Coordinate>>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        for (int l = k + 1; l < n; l++)
                        {
                            Console.WriteLine($"{i}, {j}, {k}, {l}");
                            var isSquare = IsSquare(_coordinateList[i],
                                _coordinateList[j],
                                _coordinateList[k],
                                _coordinateList[l]);

                            if (isSquare)
                            {
                                squareList.Add(
                                    new List<Coordinate>
                                    {
                                        _coordinateList[i],
                                        _coordinateList[j],
                                        _coordinateList[k],
                                        _coordinateList[l]
                                    });
                            }
                        }
                    }
                }
            }

            return squareList;
        }

        private bool IsSquare(Coordinate c1, Coordinate c2, Coordinate c3, Coordinate c4)
        {    //check four points are forming square or not
            var dist2 = GetDistance(c1, c2);     // distance from c1 to c2
            var dist3 = GetDistance(c1, c3);     // distance from c1 to c3
            var dist4 = GetDistance(c1, c4);     // distance from c1 to c4

            //when length of c1-c2 and c1-c3 are same, and square of (c1-c4) = 2*(c1-c2)
            if (dist2 == dist3 && 2 * dist2 == dist4)
            {
                var dist = GetDistance(c2, c4);
                return (dist == GetDistance(c3, c4) && dist == dist2);
            }

            //same condition for all other combinations
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
            return (c1.X - c2.X) * (c1.X - c2.X) + (c1.Y - c2.Y) * (c1.Y - c2.Y);
        }
    }
}
