using Aoc2021;
using SharpFuzz;

Fuzzer.Run(data =>
{
    using var input = new InputLineProvider(data);
    Aoc2021.D2P2.Program.MainAsync(input.GetLinesAsync()).GetAwaiter().GetResult();
});