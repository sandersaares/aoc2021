namespace Aoc2021.D1P2
{
    public sealed record DepthWindow
    {
        /// <summary>
        /// Number of measurements required before the depth window can return a result.
        /// </summary>
        public int MeasurementsRequired { get; }

        public IReadOnlyList<int> Measurements { get; private init; } = Array.Empty<int>();

        public bool HasResult => Measurements.Count == MeasurementsRequired;
        public int? Result => !HasResult ? null : Measurements.Sum();

        public DepthWindow(int measurementsRequired)
        {
            MeasurementsRequired = measurementsRequired;
        }

        public DepthWindow ExtendWith(int measurement)
        {
            var newMeasurements = (HasResult ? Measurements.Skip(1) : Measurements).ToList();
            newMeasurements.Add(measurement);

            return new DepthWindow(MeasurementsRequired)
            {
                Measurements = newMeasurements
            };
        }
    }
}
