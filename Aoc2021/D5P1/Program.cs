using Koek;

namespace Aoc2021.D5P1;

// Also part 2.
public static class Program
{
    private sealed record InputPositionPair(WorldPosition Start, WorldPosition End);

    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        // Parse the lines into point series.
        var pointSeries = (await inputLines
            .SelectAwait(line =>
            {
                // 111,222 -> 333,444

                var parts = line.Split(' ');
                if (parts.Length != 3)
                    throw new ContractException("Unexpected input line format.");

                var start = WorldPosition.Parse(parts[0]);
                var end = WorldPosition.Parse(parts[2]);

                return new ValueTask<InputPositionPair>(new InputPositionPair(start, end));
            })
            .ToListAsync())
            // Part 1: .Where(x => x.Start.IsHorizontalOrVerticalPair(x.End))
            .Select(x => new PointSeries(x.Start, x.End))
            .ToList();

        Console.WriteLine($"Loaded {pointSeries.Count} point series.");

        // Just make a dictionary of every position and its hit count. Not exactly optimal for performance but whatever.
        var hits = new Dictionary<WorldPosition, int>();

        foreach (var series in pointSeries)
        {
            foreach (var position in series.AllPositions)
            {
                if (hits.TryGetValue(position, out var oldValue))
                {
                    hits[position] = oldValue + 1;
                }
                else
                {
                    hits[position] = 1;
                }
            }
        }

        var magic = hits.Values.Count(x => x > 1);

        Console.WriteLine(magic);
    }
}
