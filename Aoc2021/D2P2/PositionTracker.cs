namespace Aoc2021.D2P2
{
    internal class PositionTracker
    {
        private int _horizontalPosition;
        private int _depth;

        private int _aim;

        public void Add(MovementCommand command)
        {
            _aim += command.AimOffset;

            _horizontalPosition += command.HorizontalOffset;
            _depth += command.HorizontalOffset * _aim;
        }

        public VehiclePosition Position => new()
        {
            HorizontalOffset = _horizontalPosition,
            DepthOffset = _depth
        };
    }
}
