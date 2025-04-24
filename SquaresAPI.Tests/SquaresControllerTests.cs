using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquaresApi.Controllers;
using SquaresAPI.Data;
using SquaresAPI.Models;
using SquaresAPI.Services;

namespace SquaresAPI.Tests
{
    public class SquaresControllerTests
    {
        private PointsContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<PointsContext>()
                .UseInMemoryDatabase("SquaresTestDb")
                .Options;
            return new PointsContext(options);
        }

        [Fact]
        public async Task Detects_Square_From_Points()
        {
            var context = GetDbContext();
            var service = new SquareDetectorService();

            context.Points.AddRange(new List<Point>
            {
                new Point { X = -1, Y = -1 },
                new Point { X = -1, Y = 1 },
                new Point { X = 1, Y = 1 },
                new Point { X = 1, Y = -1 }
            });

            await context.SaveChangesAsync();

            var controller = new SquaresController(context, service);
            var result = await controller.GetSquares();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var squares = Assert.IsType<List<List<Point>>>(okResult.Value);
            Assert.Single(squares);
        }
    }
}
