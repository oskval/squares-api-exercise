using squares_api_exercise.Models;

namespace squares_api_exercise.Contracts
{
    public interface ISquaresService
    {
        List<Coordinate> Get();
        List<Coordinate> Post(List<CoordinateDto> coordinateDtoList);
        Coordinate Add(CoordinateDto coordinateDto);
        bool Delete(string id);
        bool DeleteAll();
        List<List<Coordinate>> GetSquares();
    }
}
