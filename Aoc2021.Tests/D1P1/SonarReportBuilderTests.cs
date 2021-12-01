using Aoc2021.D1P1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2021.Tests.D1P1
{
    [TestClass]
    public sealed class SonarReportBuilderTests
    {
        [DataTestMethod]
        [DataRow(0, new int[] { })]
        [DataRow(0, new int[] { 155 })]
        [DataRow(3, new int[] { 100, 101, 102, 103 })]
        [DataRow(4, new int[] { 1, 2, 3, 1, 2, 3 })]
        public void WithVariousMeasurements_GivesExpectedIncreaseCount(int expectedResult, int[] measurements)
        {
            var builder = new SonarReportBuilder();

            foreach (var measurement in measurements)
                builder.AddMeasurement(measurement);

            var report = builder.Build();

            Assert.AreEqual(expectedResult, report.IncreaseCount);
        }
    }
}
