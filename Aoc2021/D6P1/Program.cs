namespace Aoc2021.D6P1;

public static class Program
{
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var line = await inputLines.SingleAsync();

        var fishies = line.Split(',').Select(int.Parse).ToList();

        for (var i = 0; i < 80; i++)
        {
            var updated = new List<int>(fishies.Count);

            foreach (var fish in fishies)
            {
                if (fish == 0)
                {
                    // Make a new baby fish.
                    updated.Add(8);
                    // And reset the current fish.
                    updated.Add(6);
                }
                else
                {
                    updated.Add(fish - 1);
                }
            }

            fishies = updated;
        }

        Console.WriteLine(fishies.Count);
    }
}
