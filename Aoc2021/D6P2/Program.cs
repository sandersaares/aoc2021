namespace Aoc2021.D6P2;

public static class Program
{
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var line = await inputLines.SingleAsync();

        var initial = line.Split(',').Select(int.Parse).ToList();

        // Track how many we have, indexed per lifecycle days remaining.
        var fishCounts = new long[9];

        foreach (var fish in initial)
            fishCounts[fish]++;

        // Cycle through the days.
        for (var i = 0; i < 256; i++)
        {
            // Remember for later
            var finishedCycles = fishCounts[0];

            fishCounts[0] = fishCounts[1];
            fishCounts[1] = fishCounts[2];
            fishCounts[2] = fishCounts[3];
            fishCounts[3] = fishCounts[4];
            fishCounts[4] = fishCounts[5];
            fishCounts[5] = fishCounts[6];
            fishCounts[6] = fishCounts[7] + finishedCycles;
            fishCounts[7] = fishCounts[8];
            fishCounts[8] = finishedCycles;
        }

        Console.WriteLine(fishCounts.Sum());
    }
}
