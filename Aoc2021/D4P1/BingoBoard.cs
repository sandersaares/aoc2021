using System.Text;

namespace Aoc2021.D4P1
{
    internal sealed class BingoBoard
    {
        public const int Size = 5;

        public BingoBoard(int[,] values)
        {
            if (values.GetLength(0) != Size || values.GetLength(1) != Size)
                throw new ArgumentException("Bingo board is not of expected size.");

            // Rows first.
            for (var i = 0; i < Size; i++)
            {
                var selected = new int[Size];
                for (var j = 0; j < Size; j++)
                    selected[j] = values[j, i];

                _rows[i] = new BingoArray(selected);
            }

            // Then columns.
            for (var i = 0; i < Size; i++)
            {
                var selected = new int[Size];
                for (var j = 0; j < Size; j++)
                    selected[j] = values[i, j];

                _columns[i] = new BingoArray(selected);
            }
        }

        private readonly BingoArray[] _rows = new BingoArray[Size];
        private readonly BingoArray[] _columns = new BingoArray[Size];

        public void Mark(int value)
        {
            foreach (var row in _rows)
                row.Mark(value);

            foreach (var column in _columns)
                column.Mark(value);
        }

        public bool IsWinner => _rows.Any(x => x.AllValuesAreMarked) || _columns.Any(x => x.AllValuesAreMarked);
        public int SumOfUnmarkedValues => _rows.Sum(x => x.SumOfUnmarkedValues);

        public string Dump()
        {
            var sb = new StringBuilder();

            foreach (var row in _rows)
                _ = sb.AppendLine(row.Dump());

            return sb.ToString();
        }
    }
}
