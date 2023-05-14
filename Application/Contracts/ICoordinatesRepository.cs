using Entities.DTOs;
using Entities.Models;

namespace Application.Contracts
{
    public interface ICoordinatesRepository
    {
        Task<List<Coordinate>> GetCoordinatesAsync();
        Task<Coordinate> SaveCoordinateAsync(CoordinateDto coordinateDto);
        Task<List<Coordinate>> SaveCoordinatesAsync(List<CoordinateDto> coordinateDtos);
        Task<bool> DeleteCoordinateAsync(string id);
        Task<bool> DeleteCoordinatesAsync();
    }
}
