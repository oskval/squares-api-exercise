using Microsoft.AspNetCore.Mvc;
using squares_api_exercise.Contracts;
using squares_api_exercise.Models;

namespace squares_api_exercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SquaresController : ControllerBase
    {
        private readonly ISquaresService _squaresService;

        public SquaresController(ISquaresService squaresService)
        {
            _squaresService = squaresService;
        }

        [HttpGet]
        public async Task<List<Coordinate>> Get()
        {
            return _squaresService.Get();
        }

        [HttpPost]
        public async Task<List<Coordinate>> Post([FromBody] List<CoordinateDto> coordinates)
        {
            return _squaresService.Post(coordinates);
        }

        [HttpPost("AddCoordinate")]
        public async Task<Coordinate> Add([FromBody] CoordinateDto coordinateDto)
        {
            return _squaresService.Add(coordinateDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return _squaresService.Delete(id) ? Ok() : BadRequest();
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            return _squaresService.DeleteAll() ? Ok() : BadRequest();
        }
    }
}