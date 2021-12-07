using Koek;
using System.Text;

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

    // bias is which bit to choose if both are equally common
    private static void FindMostAndLeastCommonBits(int bitCount, int[] onCount, int[] offCount, char bias, out string mostCommon, out string leastCommon, out int mostCommonDecimal, out int leastCommonDecimal)
    {
        // gamma
        var mostCommonBuilder = new StringBuilder();
        // epsilon
        var leastCommonBuilder = new StringBuilder();

        for (var i = 0; i < bitCount; i++)
        {
            if (onCount[i] > offCount[i])
            {
                _ = mostCommonBuilder.Append('1');
                _ = leastCommonBuilder.Append('0');
            }
            else if (onCount[i] < offCount[i])
            {
                _ = mostCommonBuilder.Append('0');
                _ = leastCommonBuilder.Append('1');
            }
            else
            {
                mostCommonBuilder.Append(bias);
                leastCommonBuilder.Append(bias);
            }
        }

        mostCommon = mostCommonBuilder.ToString();
        leastCommon = leastCommonBuilder.ToString();

        Console.WriteLine(mostCommon);
        Console.WriteLine(leastCommon);

        mostCommonDecimal = Convert.ToInt32(mostCommon, fromBase: 2);
        leastCommonDecimal = Convert.ToInt32(leastCommon, fromBase: 2);

        Console.WriteLine(mostCommonDecimal);
        Console.WriteLine(leastCommonDecimal);
    }

    private static void CountBitsInPosition(List<string> lines, int bitCount, out int[] onCount, out int[] offCount)
    {
        onCount = new int[bitCount];
        offCount = new int[bitCount];

        foreach (var line in lines)
        {
            for (var i = 0; i < bitCount; i++)
            {
                if (line[i] == '1')
                    onCount[i]++;
                else if (line[i] == '0')
                    offCount[i]++;
                else
                    throw new ContractException($"Not a bit value: {line[i]}");
            }
        }
    }

    private static string SingleWithGreatestPrefix(List<string> lines, string prefix)
    {
        for (var prefixLength = 1; prefixLength < prefix.Length; prefixLength++)
        {
            var subPrefix = prefix.Substring(0, prefixLength);

            var matches = lines.Where(x => x.StartsWith(subPrefix, StringComparison.Ordinal)).ToList();

            Console.WriteLine($"Matching against {subPrefix} gave {matches.Count} results.");

            if (matches.Count == 1)
            {
                Console.WriteLine($"Selected {matches[0]}.");
                return matches[0];
            }
        }

        throw new ContractException("Did not find a single value that matched the search criterium.");
    }
}
