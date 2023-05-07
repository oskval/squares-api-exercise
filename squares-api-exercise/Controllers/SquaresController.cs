using Microsoft.AspNetCore.Mvc;
using squares_api_exercise.Contracts;

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
        public Task<List<string>> Get()
        {
            return Task.FromResult(_squaresService.GetSquares());
        }
    }
}