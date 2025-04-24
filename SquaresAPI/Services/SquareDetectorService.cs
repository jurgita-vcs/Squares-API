using SquaresAPI.Models;

namespace SquaresAPI.Services
{
    public class SquareDetectorService
    {
        public List<List<Point>> FindSquares(List<Point> points)
        {
            var squares = new List<List<Point>>();
            var seen = new HashSet<string>();
            int n = points.Count;

            for (int a = 0; a < n - 3; a++)
                for (int b = a + 1; b < n - 2; b++)
                    for (int c = b + 1; c < n - 1; c++)
                        for (int d = c + 1; d < n; d++)
                        {
                            var quad = new[] { points[a], points[b], points[c], points[d] };
                            if (!IsSquare(quad)) continue;

                            var key = NormalizeSquare(quad);
                            if (seen.Add(key)) // Add returns true if key was not already in the set
                                squares.Add(quad.ToList());
                        }

            return squares;
        }

        private string NormalizeSquare(Point[] points)
        {
            return string.Join("|", points
                .OrderBy(p => p.X)
                .ThenBy(p => p.Y)
                .Select(p => $"{p.X},{p.Y}"));
        }

        private bool IsSquare(Point[] p)
        {
            var distances = new List<int>();

            // Calculate squared distances between all 6 pairs of points
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    distances.Add(DistanceSquared(p[i], p[j]));
                }
            }

            distances.Sort();

            // In a square:
            // 4 sides equal (first 4)
            // 2 diagonals equal (last 2), and longer
            return distances[0] > 0 &&
                   distances[0] == distances[1] &&
                   distances[1] == distances[2] &&
                   distances[2] == distances[3] &&
                   distances[4] == distances[5] &&
                   distances[4] > distances[0];
        }

        private int DistanceSquared(Point a, Point b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }
    }
}

