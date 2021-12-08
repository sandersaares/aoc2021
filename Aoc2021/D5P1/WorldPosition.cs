namespace Aoc2021.D5P1;

internal sealed record WorldPosition(int X, int Y)
{
    public static WorldPosition Parse(string s)
    {
        // 1111,2222
        var parts = s.Split(',');
        var x = int.Parse(parts[0]);
        var y = int.Parse(parts[1]);

        return new WorldPosition(x, y);
    }

    public bool IsHorizontalOrVerticalPair(WorldPosition other)
    {
        return other.X == X || other.Y == Y;
    }
}