using SquaresAPI.Models;
using SquaresAPI.Services;

namespace SquaresAPI.Tests
{
    public class SquareDetectorServiceTests
    {
        private readonly SquareDetectorService _service = new SquareDetectorService();

        [Fact]
        public void Detects_Single_Square()
        {
            var points = new List<Point>
            {
                new Point { X = -1, Y = -1 },
                new Point { X = -1, Y = 1 },
                new Point { X = 1, Y = 1 },
                new Point { X = 1, Y = -1 }
            };

            var result = _service.FindSquares(points);
            Assert.Single(result);
        }

        [Fact]
        public void Detects_No_Squares_From_Noise()
        {
            var points = new List<Point>
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 2, Y = 2 },
                new Point { X = 3, Y = 1 },
                new Point { X = 5, Y = 8 }
            };

            var result = _service.FindSquares(points);
            Assert.Empty(result);
        }

        [Fact]
        public void Detects_Squares_Ignores_Duplicates()
        {
            var points = new List<Point>
            {
                new Point { X = -1, Y = -1 },
                new Point { X = -1, Y = 1 },
                new Point { X = 1, Y = 1 },
                new Point { X = 1, Y = -1 },

                // Same square again, different order
                new Point { X = 1, Y = -1 },
                new Point { X = -1, Y = 1 },
                new Point { X = -1, Y = -1 },
                new Point { X = 1, Y = 1 }
            };

            var result = _service.FindSquares(points);
            Assert.Single(result);
        }

        [Fact]
        public void Handles_Empty_List()
        {
            var result = _service.FindSquares(new List<Point>());
            Assert.Empty(result);
        }

    }
}