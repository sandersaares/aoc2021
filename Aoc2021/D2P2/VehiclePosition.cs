namespace Aoc2021.D2P2
{
    internal sealed record VehiclePosition
    {
        /// <summary>
        /// Incrementing to the right.
        /// </summary>
        public int HorizontalOffset { get; init; }

        /// <summary>
        /// Bigger number is down.
        /// </summary>
        public int DepthOffset { get; init; }
    }
}
