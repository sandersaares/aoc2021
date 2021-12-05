using System.Text;

namespace Aoc2021
{
    public sealed class InputLineProvider : IDisposable
    {
        public InputLineProvider(Stream input)
        {
            _reader = new StreamReader(input, Encoding.UTF8);
        }

        public InputLineProvider(string path)
        {
            _reader = new StreamReader(path, Encoding.UTF8);
        }

        private readonly StreamReader _reader;

        public void Dispose()
        {
            _reader.Dispose();
        }

        public async IAsyncEnumerable<string> GetLinesAsync()
        {
            while (!_reader.EndOfStream)
            {
                var line = await _reader.ReadLineAsync();

                if (line == null)
                    continue;

                yield return line;
            }
        }
    }
}
