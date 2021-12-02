using Koek;

namespace Aoc2021.D2P2;

internal static class Program
{
    public static async Task MainAsync()
    {
        var tracker = new PositionTracker();

        using var reader = new StreamReader("D2P1/input.txt");

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();

            if (line == null)
                continue;

            var vector = MovementCommand.Parse(line);
            tracker.Add(vector);
        }

        var finalPosition = tracker.Position;
        Console.WriteLine(Helpers.Debug.ToDebugString(finalPosition));

        var result = finalPosition.DepthOffset * finalPosition.HorizontalOffset;
        Console.WriteLine(result);
    }
}
