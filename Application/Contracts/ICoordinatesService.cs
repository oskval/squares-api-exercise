using Entities.DTOs;
using Entities.Models;

namespace Application.Contracts
{
    public interface ICoordinatesService
    {
        List<Coordinate> GetCoordinates();
        List<Coordinate> SaveCoordinates(List<CoordinateDto> coordinateDtoList);
        Coordinate SaveCoordinate(CoordinateDto coordinateDto);
        bool DeleteCoordinate(string id);
        bool DeleteCoordinates();
        List<List<Coordinate>> GetSquares();
    }
}
