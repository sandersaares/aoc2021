using Koek;

namespace Aoc2021.D5P1;

internal sealed class PointSeries
{
    public WorldPosition Start { get; }
    public WorldPosition End { get; }

    public IReadOnlyCollection<WorldPosition> AllPositions { get; }

    public PointSeries(WorldPosition start, WorldPosition end)
    {
        Start = start;
        End = end;

        AllPositions = FindAllPositions(start, end).ToList();
    }

    private static IEnumerable<WorldPosition> FindAllPositions(WorldPosition a, WorldPosition b)
    {
        if (a.X == b.X)
        {
            // Vertical.
            var start = Math.Min(a.Y, b.Y);
            var end = Math.Max(a.Y, b.Y);

            for (var y = start; y <= end; y++)
                yield return new WorldPosition(a.X, y);
        }
        else if (a.Y == b.Y)
        {
            // Horizontal.
            var start = Math.Min(a.X, b.X);
            var end = Math.Max(a.X, b.X);

            for (var x = start; x <= end; x++)
                yield return new WorldPosition(x, a.Y);
        }
        else
        {
            // 45 degree diagonal. We'll skip validating 45 degreeness because whatver.
            var stepX = a.X < b.X ? 1 : -1;
            var stepY = a.Y < b.Y ? 1 : -1;

            var x = a.X;
            var y = a.Y;

            while (x != b.X + stepX)
            {
                yield return new WorldPosition(x, y);

                x += stepX;
                y += stepY;
            }
        }
    }
}
