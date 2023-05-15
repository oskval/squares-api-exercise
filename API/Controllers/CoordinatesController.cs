using Application.Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // TODO: Add generic base response class

    [ApiController]
    [Route("[controller]")]
    public class CoordinatesController : ControllerBase
    {
        private readonly ICoordinatesService _coordinatesService;

        public CoordinatesController(ICoordinatesService coordinatesService)
        {
            _coordinatesService = coordinatesService;
        }

        [HttpGet]
        public async Task<List<Coordinate>> GetCoordinates()
        {
            return await _coordinatesService.GetCoordinates();
        }

        [HttpPost]
        public async Task<List<Coordinate>> PostCoordinates([FromBody] List<CoordinateDto> coordinates)
        {
            return await _coordinatesService.SaveCoordinates(coordinates);
        }

        [HttpPost("AddCoordinate")]
        public async Task<Coordinate> PostCoordinate([FromBody] CoordinateDto coordinateDto)
        {
            return await _coordinatesService.SaveCoordinate(coordinateDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinate(string id)
        {
            return await _coordinatesService.DeleteCoordinate(id) ? Ok() : BadRequest();
        }

        [HttpDelete("DeleteCoordinates")]
        public async Task<IActionResult> DeleteCoordinates()
        {
            return await _coordinatesService.DeleteCoordinates() ? Ok() : BadRequest();
        }
    }
}