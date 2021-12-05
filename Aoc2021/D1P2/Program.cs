namespace Aoc2021.D1P2;

public static class Program
{
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var reportBuilder = new SonarReportBuilder();

        await foreach (var line in inputLines)
        {
            var depth = int.Parse(line);
            reportBuilder.AddMeasurement(depth);
        }

        var report = reportBuilder.Build();

        Console.WriteLine(report.IncreaseCount);
    }
}
