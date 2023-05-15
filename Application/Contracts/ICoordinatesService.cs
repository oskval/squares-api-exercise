using Entities.DTOs;
using Entities.Models;

namespace Application.Contracts
{
    public interface ICoordinatesService
    {
        Task<List<Coordinate>> GetCoordinates();
        Task<List<Coordinate>> SaveCoordinates(List<CoordinateDto> coordinateDtoList);
        Task<Coordinate> SaveCoordinate(CoordinateDto coordinateDto);
        Task<bool> DeleteCoordinate(string id);
        Task<bool> DeleteCoordinates();
    }
}
