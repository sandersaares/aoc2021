using Koek;

namespace Aoc2021.D4P1;

public static class Program
{
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

        foreach (var number in numbers)
        {
            Console.WriteLine($"Marking {number}");

            foreach (var board in boards)
                board.Mark(number);

            var winner = boards.SingleOrDefault(x => x.IsWinner);

            if (winner != null)
            {
                Console.WriteLine($"Found winner!");
                Console.WriteLine(winner.Dump());

                var magicValue = winner.SumOfUnmarkedValues * number;
                Console.WriteLine(magicValue);

                return;
            }
        }

        throw new ContractException("No winner found.");
    }
}
