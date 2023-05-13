using Application.Contracts;
using Entities.DTOs;
using Entities.Models;

namespace Application.Services
{
    public class CoordinateService : ICoordinatesService
    {
        // TODO: move to database
        private List<Coordinate> _coordinateList = new();

        public List<Coordinate> GetCoordinates()
        {
            if (!_coordinateList.Any())
            {
                throw new Exception("Please add a coordinates and try again.");
            }
            return _coordinateList;
        }

        public Coordinate SaveCoordinate(CoordinateDto coordinateDto)
        {
            var coordinate = new Coordinate
            {
                Id = "asd",
                XCoord = coordinateDto.XCoord,
                YCoord = coordinateDto.YCoord,
            };

            _coordinateList.Add(coordinate);

            return coordinate;
        }

        public List<Coordinate> SaveCoordinates(List<CoordinateDto> coordinateDtoList)
        {
            var coordinateList = coordinateDtoList.Select(x =>
            {
                var coordinate = new Coordinate
                {
                    XCoord = x.XCoord,
                    YCoord = x.YCoord,
                };

                return coordinate;
            }).ToList();

            _coordinateList = coordinateList;
            return coordinateList;
        }

        public bool DeleteCoordinate(string id)
        {
            var coordinate = _coordinateList.FirstOrDefault(x => x.Id == id);

            if (coordinate == null)
            {
                throw new Exception($"No Coordinate found by given id: {id}");
            }

            _coordinateList.Remove(coordinate); 
            return true;
        }

        public bool DeleteCoordinates()
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

            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    for (var k = j + 1; k < n; k++)
                    {
                        for (var l = k + 1; l < n; l++)
                        {
                            var isSquare = IsSquare(
                                _coordinateList[i],
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
            return (c1.XCoord - c2.XCoord) * (c1.XCoord - c2.XCoord) + (c1.YCoord - c2.YCoord) * (c1.YCoord - c2.YCoord);
        }
    }
}
