using Entities.Models;
namespace Application.Contracts;

public interface IShapesService
{
    Task<List<List<Coordinate>>> GetSquares();
}