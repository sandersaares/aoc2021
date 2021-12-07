using Koek;

namespace Aoc2021.D3P2;

public static class Program
{
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var lines = await inputLines.ToListAsync();

        // We assume every line has the same number of bits.
        var bitCount = lines.First().Length;

        if (bitCount <= 0)
            throw new ContractException("No bits found on first line.");

        var oxygenGeneratorRating = FindByPrefixSearch(lines, GetOxygenGeneratorPrefix);
        var co2ScrubberRating = FindByPrefixSearch(lines, GetCo2ScrubberPrefix);

        var oxygenGeneratorRatingDecimal = Convert.ToInt32(oxygenGeneratorRating, fromBase: 2);
        var co2ScrubberRatingDecimal = Convert.ToInt32(co2ScrubberRating, fromBase: 2);

        Console.WriteLine(oxygenGeneratorRatingDecimal);
        Console.WriteLine(co2ScrubberRatingDecimal);

        var lifeSupportRating = oxygenGeneratorRatingDecimal * co2ScrubberRatingDecimal;
        Console.WriteLine(lifeSupportRating);
    }

    private static string FindByPrefixSearch(List<string> lines, Func<List<string>, string, string> getPrefix)
    {
        string prefix = "";

        while (lines.Any())
        {
            // Get prefix from list of remaining items + existing prefix.
            prefix = getPrefix(lines, prefix);

            var matches = lines.Where(x => x.StartsWith(prefix, StringComparison.Ordinal)).ToList();

            if (matches.Count == 1)
            {
                Console.WriteLine($"Found {matches[0]}");
                return matches[0];
            }

            Console.WriteLine($"Searching through remaining {lines.Count} items found {matches.Count} matches.");

            lines = matches;
        }

        throw new ContractException("Ran out of candidates during search.");
    }

    private static string GetOxygenGeneratorPrefix(List<string> lines, string existingPrefix)
        => GetPrefix(lines, existingPrefix, mostCommon: true, bias: '1');

    private static string GetCo2ScrubberPrefix(List<string> lines, string existingPrefix)
        => GetPrefix(lines, existingPrefix, mostCommon: false, bias: '0');

    private static string GetPrefix(List<string> lines, string existingPrefix, bool mostCommon, char bias)
    {
        var oneCount = 0;
        var zeroCount = 0;

        foreach (var line in lines)
        {
            if (line[existingPrefix.Length] == '0')
                zeroCount++;
            else
                oneCount++;
        }

        if (oneCount == zeroCount)
            return existingPrefix + bias;

        bool returnOne = oneCount > zeroCount;

        // If we want the least common, just flip the result.
        if (!mostCommon)
            returnOne = !returnOne;

        if (returnOne)
            return existingPrefix + '1';
        else
            return existingPrefix + '0';
    }
}
