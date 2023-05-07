using squares_api_exercise.Contracts;

namespace squares_api_exercise.Services
{
    public class SquaresService : ISquaresService
    {
        public List<string> GetSquares()
        {
            var strings = new List<string>() { "123123", "123123", "1233123" };
            return strings;
        }
    }
}
