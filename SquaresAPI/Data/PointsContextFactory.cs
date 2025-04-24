using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SquaresAPI.Data
{
    public class PointsContextFactory : IDesignTimeDbContextFactory<PointsContext>
    {
        public PointsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PointsContext>();
            optionsBuilder.UseSqlite("Data Source=points.db");

            return new PointsContext(optionsBuilder.Options);
        }
    }
}