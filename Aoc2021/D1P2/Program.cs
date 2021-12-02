namespace Aoc2021.D1P2;

internal static class Program
{
    public static async Task MainAsync()
    {
        var reportBuilder = new SonarReportBuilder();

        using var reader = new StreamReader("D1P2/input.txt");

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();

            if (line == null)
                continue;

            var depth = int.Parse(line);
            reportBuilder.AddMeasurement(depth);
        }

        var report = reportBuilder.Build();

        Console.WriteLine(report.IncreaseCount);
    }
}
