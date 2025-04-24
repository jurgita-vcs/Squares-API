using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquaresAPI.Data;
using SquaresAPI.Models;


namespace SquaresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly PointsContext _context;

        public PointsController(PointsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Bulk import a list of points.
        /// </summary>
        [HttpPost("import")]
        public async Task<IActionResult> ImportPoints([FromBody] List<Point> points)
        {
            _context.Points.AddRange(points);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Add a single point to the existing list.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddPoint([FromBody] Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();
            return Ok(point);
        }

        /// <summary>
        /// Delete a point by ID from an existing list.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoint(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point == null) return NotFound();
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Retrieve points list
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
        {
            return await _context.Points.ToListAsync();
        }
    }
}

