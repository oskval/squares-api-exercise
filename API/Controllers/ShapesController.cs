using Application.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // TODO: Add generic base response class

    [ApiController]
    [Route("[controller]")]
    public class ShapesController : ControllerBase
    {
        private readonly IShapesService _shapesService;

        public ShapesController(IShapesService shapesService)
        {
            _shapesService = shapesService;
        }

        [HttpGet("GetSquares")]
        public async Task<List<List<Coordinate>>> GetSquares()
        {
            return await _shapesService.GetSquares();
        }
    }
}