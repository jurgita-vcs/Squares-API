using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquaresAPI.Controllers;
using SquaresAPI.Data;
using SquaresAPI.Models;

namespace SquaresAPI.Tests
{
    public class PointsControllerTests
    {
        private PointsContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<PointsContext>()
                .UseInMemoryDatabase(databaseName: "PointsTestDb")
                .Options;
            return new PointsContext(options);
        }

        [Fact]
        public async Task Can_Add_And_Get_Point()
        {
            var context = GetDbContext();
            var controller = new PointsController(context);

            var point = new Point { X = 5, Y = 10 };
            await controller.AddPoint(point);

            var result = await controller.GetPoints();
            var actionResult = await controller.GetPoints();
            var okValue = Assert.IsAssignableFrom<IEnumerable<Point>>(actionResult.Value);
            var list = okValue.ToList();

            Assert.Single(list);
            Assert.Equal(5, list[0].X);
            Assert.Equal(10, list[0].Y);
        }

        [Fact]
        public async Task Can_Delete_Point()
        {
            // Use a unique DB name per test to avoid shared state
            var options = new DbContextOptionsBuilder<PointsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PointsContext(options);
            var point = new Point { X = 1, Y = 2 };
            context.Points.Add(point);
            await context.SaveChangesAsync();

            var controller = new PointsController(context);

            var result = await controller.DeletePoint(point.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(await context.Points.ToListAsync());
        }
    }
}
