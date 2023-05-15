using Application.Contracts;
using Entities.DTOs;
using Entities.Models;

namespace Application.Services
{
    // TODO: Add validation, exceptions and middleware handling
    public class CoordinatesService : ICoordinatesService
    {
        private readonly ICoordinatesRepository _coordinatesRepository;

        public CoordinatesService(ICoordinatesRepository coordinatesRepository)
        {
            _coordinatesRepository = coordinatesRepository;
        }

        public async Task<List<Coordinate>> GetCoordinates()
        {
            return await _coordinatesRepository.GetCoordinatesAsync();
        }

        public async Task<Coordinate> SaveCoordinate(CoordinateDto coordinateDto)
        {
            return await _coordinatesRepository.SaveCoordinateAsync(coordinateDto);
        }

        public async Task<List<Coordinate>> SaveCoordinates(List<CoordinateDto> coordinateDtos)
        {
            return await _coordinatesRepository.SaveCoordinatesAsync(coordinateDtos);
        }

        public async Task<bool> DeleteCoordinate(string id)
        {
            return await _coordinatesRepository.DeleteCoordinateAsync(id);
        }

        public async Task<bool> DeleteCoordinates()
        {
            return await _coordinatesRepository.DeleteCoordinatesAsync();
        }
    }
}
