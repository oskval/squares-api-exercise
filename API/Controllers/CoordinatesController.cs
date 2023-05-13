using Application.Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
            return _coordinatesService.GetCoordinates();
        }

        [HttpPost]
        public async Task<List<Coordinate>> PostCoordinates([FromBody] List<CoordinateDto> coordinates)
        {
            return _coordinatesService.SaveCoordinates(coordinates);
        }

        [HttpPost("AddCoordinate")]
        public async Task<Coordinate> PostCoordinate([FromBody] CoordinateDto coordinateDto)
        {
            return _coordinatesService.SaveCoordinate(coordinateDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinate(string id)
        {
            return _coordinatesService.DeleteCoordinate(id) ? Ok() : BadRequest();
        }

        [HttpDelete("DeleteCoordinates")]
        public async Task<IActionResult> DeleteCoordinates()
        {
            return _coordinatesService.DeleteCoordinates() ? Ok() : BadRequest();
        }

        [HttpGet("GetSquares")]
        public async Task<List<List<Coordinate>>> GetSquares()
        {
            return _coordinatesService.GetSquares();
        }
    }
}