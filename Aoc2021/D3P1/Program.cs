using Koek;
using System.Text;

namespace Aoc2021.D3P1;

public static class Program
{
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var lines = await inputLines.ToListAsync();

        // We assume every line has the same number of bits.
        var bitCount = lines.First().Length;

        if (bitCount <= 0)
            throw new ContractException("No bits found on first line.");

        CountBitsInPosition(lines, bitCount, out int[] onCount, out int[] offCount);
        FindMostAndLeastCommonBits(bitCount, onCount, offCount, out int gamma, out int epsilon);

        var powerConsumption = gamma * epsilon;
        Console.WriteLine(powerConsumption);
    }

    // bias is which bit to choose if both are equally common
    private static void FindMostAndLeastCommonBits(int bitCount, int[] onCount, int[] offCount, out int mostCommonDecimal, out int leastCommonDecimal)
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
            else
            {
                _ = mostCommonBuilder.Append('0');
                _ = leastCommonBuilder.Append('1');
            }
        }

        var mostCommon = mostCommonBuilder.ToString();
        var leastCommon = leastCommonBuilder.ToString();

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
}
