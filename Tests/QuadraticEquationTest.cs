using System;
using Moq;
using PatternFlow;
using PatternFlow.QuadraticEquation;

namespace Tests;

public class QuadraticEquationTests
{
    private Mock<ILogger> _mockLogger;
    private QuadraticEquation _solver;

    public QuadraticEquationTests()
    {
        _mockLogger = new Mock<ILogger>();
        _solver = new QuadraticEquation(_mockLogger.Object);
    }

    [Fact]
    public void Test_NoRoots()
    {
        double[] result = _solver.Solve(1, 0, 1);
        Assert.Empty(result);
    }

    [Fact]
    public void Test_TwoDistinctRoots()
    {
        double[] result = _solver.Solve(1, 0, -1);
        Assert.Equal(2, result.Length);
        Assert.Contains(1.0, result);
        Assert.Contains(-1.0, result);
    }

    [Fact]
    public void Test_DoubleRoot()
    {
        double[] result = _solver.Solve(1, 2, 1);
        Assert.Single(result);
        Assert.Equal(-1.0, result[0], 10);
    }

    [Fact]
    public void Test_CoefficientAIsZero()
    {
        Assert.Throws<ArgumentException>(() => _solver.Solve(0, 2, 1));
        _mockLogger.Verify(logger => logger.Log("Значение а не может быть равно 0"), Times.Once);
    }

    [Fact]
    public void Test_NonNumericCoefficients()
    {
        Assert.Throws<ArgumentException>(() => _solver.Solve(double.NaN, 2, 1));
        Assert.Throws<ArgumentException>(() => _solver.Solve(1, double.PositiveInfinity, 1));
        Assert.Throws<ArgumentException>(() => _solver.Solve(1, 2, double.NegativeInfinity));
    }
}