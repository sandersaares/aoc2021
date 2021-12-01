namespace Aoc2021.D1P2;

public sealed class SonarReportBuilder
{
    private DepthWindow _lastWindow = new(measurementsRequired: 3);
    private int _depthIncreases;

    public void AddMeasurement(int depth)
    {
        var newWindow = _lastWindow.ExtendWith(depth);

        try
        {
            if (!_lastWindow.HasResult)
                return;

            if (_lastWindow.Result < newWindow.Result)
                _depthIncreases++;
        }
        finally
        {
            _lastWindow = newWindow;
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