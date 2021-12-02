namespace Aoc2021.D2P1
{
    internal sealed record MovementVector
    {
        /// <summary>
        /// Incrementing to the right.
        /// </summary>
        public int HorizontalOffset { get; init; }

        /// <summary>
        /// Bigger number is down.
        /// </summary>
        public int DepthOffset { get; init; }

        public static MovementVector Parse(string commandInput)
        {
            // down 5
            // up 9
            // forward 1
            var parts = commandInput.Split(' ');

            if (parts.Length != 2)
                throw new ArgumentException($"Could not parse command input: {commandInput}", nameof(commandInput));

            var value = int.Parse(parts[1]);

            return parts[0] switch
            {
                "down" => new MovementVector
                {
                    DepthOffset = value
                },
                "up" => new MovementVector
                {
                    DepthOffset = -value
                },
                "forward" => new MovementVector
                {
                    HorizontalOffset = value
                },
                _ => throw new ArgumentException($"Invalid command: {parts[0]}", nameof(commandInput))
            };
        }
    }
}
