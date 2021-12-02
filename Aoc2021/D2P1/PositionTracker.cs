namespace Aoc2021.D2P1
{
    internal class PositionTracker
    {
        private int _horizontalPosition;
        private int _depth;

        public void Add(MovementVector vector)
        {
            _horizontalPosition += vector.HorizontalOffset;
            _depth += vector.DepthOffset;
        }

        public MovementVector Position => new()
        {
            HorizontalOffset = _horizontalPosition,
            DepthOffset = _depth
        };
    }
}
