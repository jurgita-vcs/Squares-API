using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquaresAPI.Data;
using SquaresAPI.Models;
using SquaresAPI.Services;

namespace SquaresApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SquaresController : ControllerBase
    {
        private readonly PointsContext _context;
        private readonly SquareDetectorService _squareService;

        public SquaresController(PointsContext context, SquareDetectorService squareService)
        {
            _context = context;
            _squareService = squareService;
        }

        /// <summary>
        /// Get the list of unique squares identified.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<List<Point>>>> GetSquares()
        {
            var points = await _context.Points.ToListAsync();
            var squares = _squareService.FindSquares(points);
            return Ok(squares);
        }
    }
}
