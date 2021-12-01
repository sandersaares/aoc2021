using Aoc2021.D1P2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Aoc2021.Tests.D1P2
{
    [TestClass]
    public sealed class SonarReportBuilderTests
    {
        [DataTestMethod]
        [DataRow(0, new int[] { })]
        [DataRow(0, new int[] { 155 })]
        [DataRow(0, new int[] { 155, 155, 156 })]
        [DataRow(0, new int[] { 155, 155, 156, 155 })]
        [DataRow(1, new int[] { 155, 155, 156, 156 })]
        [DataRow(2, new int[] { 155, 155, 156, 156, 156 })]
        [DataRow(2, new int[] { 155, 155, 156, 156, 156, 156 })]
        public void WithVariousMeasurements_GivesExpectedIncreaseCount(int expectedResult, int[] measurements)
        {
            var builder = new SonarReportBuilder();

            if (expectedResult == 0 && measurements.Length == 0)
                throw new NotImplementedException();

            foreach (var measurement in measurements)
                builder.AddMeasurement(measurement);

            var report = builder.Build();

            Assert.AreEqual(expectedResult, report.IncreaseCount);
        }
    }
}
