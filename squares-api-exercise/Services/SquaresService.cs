using System.Reflection.Metadata.Ecma335;
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

            _coordinateList.AddRange(coordinateList);
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

    }
}
