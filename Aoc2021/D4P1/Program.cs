using Koek;

namespace Aoc2021.D4P1;

public static class Program
{
    // Also P2 because it's easy.
    public static async Task MainAsync(IAsyncEnumerable<string> inputLines)
    {
        var lines = await inputLines.ToListAsync();

        // First line is the drawn numbers.
        var numbers = lines[0].Split(',').Select(int.Parse);

        // Then we have the boards, interspersed by empty lines.
        lines = lines.Skip(2).ToList();

        var boards = new List<BingoBoard>();

        // 5 lines of body, 1 empty line
        for (var i = 0; i < lines.Count; i += BingoBoard.Size + 1)
        {
            var values = new int[BingoBoard.Size, BingoBoard.Size];

            for (var row = 0; row < BingoBoard.Size; row++)
            {
                var rowValues = lines[i + row].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                for (var column = 0; column < BingoBoard.Size; column++)
                    values[column, row] = rowValues[column];
            }

            boards.Add(new BingoBoard(values));
        }

        Console.WriteLine($"Loaded {boards.Count} boards.");

        bool firstWinnerFound = false;

        foreach (var number in numbers)
        {
            Console.WriteLine($"Marking {number}");

            foreach (var board in boards)
                board.Mark(number);

            var winners = boards.Where(x => x.IsWinner).ToList();

            foreach (var winner in winners)
            {
                if (!firstWinnerFound || boards.Count == 1)
                {
                    Console.WriteLine($"Found winner!");
                    Console.WriteLine(winner.Dump());

                    var magicValue = winner.SumOfUnmarkedValues * number;
                    Console.WriteLine($"Winner magic value: {magicValue}");
                    firstWinnerFound = true;
                }

                // Keep going so we can find the board that wins the last.
                boards.Remove(winner);

                if (boards.Count == 0)
                    return; // All done!
            }
        }

        throw new UnreachableCodeException();
    }
}
