namespace Aoc2021.D1P1;

public sealed class SonarReportBuilder
{
    private int? _lastMeasurement;
    private int _depthIncreases;

    public void AddMeasurement(int depth)
    {
        try
        {
            if (_lastMeasurement == null)
                return;

            if (_lastMeasurement < depth)
                _depthIncreases++;
        }
        finally
        {
            _lastMeasurement = depth;
        }
    }

    public SonarReport Build()
    {
        return new SonarReport
        {
            IncreaseCount = _depthIncreases
        };
    }
}