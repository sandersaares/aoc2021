namespace Aoc2021.D4P1
{
    /// <summary>
    /// An array of values, some of which may be marked and others unmarked (representing either a row or column of a bingo board).
    /// </summary>
    internal sealed class BingoArray
    {
        public bool AllValuesAreMarked => !_unmarked.Any();
        public int SumOfUnmarkedValues => _unmarked.Sum();

        private readonly List<int> _unmarked = new();
        private readonly List<int> _marked = new();

        private readonly List<int> _values;

        public BingoArray(IEnumerable<int> values)
        {
            _unmarked.AddRange(values);
            _values = values.ToList();
        }

        public void Mark(int value)
        {
            if (_unmarked.Remove(value))
                _marked.Add(value);
        }

        public string Dump()
        {
            return string.Join(" ", _values);
        }
    }
}
