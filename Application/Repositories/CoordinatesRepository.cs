using Application.Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories
{
    // TODO: Add Mapper
    // TODO: Make Generic

    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly DataContext _dataContext;

        public CoordinatesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Coordinate>> GetCoordinatesAsync()
        {
            return await _dataContext.Coordinates.ToListAsync();
        }

        public async Task<Coordinate> SaveCoordinateAsync(CoordinateDto coordinateDto)
        {
            var coordinate = new Coordinate
            {
                XCoord = coordinateDto.XCoord,
                YCoord = coordinateDto.YCoord
            };

            _dataContext.Add(coordinate);

            await _dataContext.SaveChangesAsync();
            return coordinate;
        }

        public async Task<List<Coordinate>> SaveCoordinatesAsync(List<CoordinateDto> coordinateDtos)
        {
            var coordinateList = coordinateDtos.Select(x =>
                new Coordinate
                {
                    XCoord = x.XCoord,
                    YCoord = x.YCoord
                }).ToList();

            _dataContext.AddRange(coordinateList);
            await _dataContext.SaveChangesAsync();
            return coordinateList;
        }

        public async Task<bool> DeleteCoordinateAsync(string id)
        {
            var coordinate = await _dataContext.Coordinates.FindAsync(id);

            if (coordinate == null)
                return false;

            _dataContext.Remove(coordinate);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCoordinatesAsync()
        {
            var allCoordinates = await _dataContext.Coordinates.ToListAsync();

            if (allCoordinates.Count > 0)
            {
                _dataContext.Coordinates.RemoveRange(allCoordinates);
                return await _dataContext.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
