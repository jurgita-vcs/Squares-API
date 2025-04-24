using Microsoft.EntityFrameworkCore;
using SquaresAPI.Models;

namespace SquaresAPI.Data
{
    public class PointsContext : DbContext
    {
        public PointsContext(DbContextOptions<PointsContext> options) : base(options) { }
        public DbSet<Point> Points { get; set; }
    }
}
