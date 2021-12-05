using Koek;

namespace Aoc2021.D2P2;

public static class Program
{
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var tracker = new PositionTracker();

        await foreach (var line in inputLines)
        {
            var vector = MovementCommand.Parse(line);
            tracker.Add(vector);
        }

        var finalPosition = tracker.Position;
        Console.WriteLine(Helpers.Debug.ToDebugString(finalPosition));

        var result = finalPosition.DepthOffset * finalPosition.HorizontalOffset;
        Console.WriteLine(result);
    }
}
