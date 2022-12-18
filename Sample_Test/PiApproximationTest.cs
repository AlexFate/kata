using System;
using CodeWarsFSharp;
using Xunit;

namespace Sample_Test;

public class PiApproximationTest
{
    [Theory]
    [InlineData(0.1, 10, 3.0418396189)]
    [InlineData(0.01, 100, 3.1315929035)]
    [InlineData(0.005, 200, 3.1365926848)]
    [InlineData(0.001, 1000, 3.1405926538)]
    [InlineData(0.0001, 10000, 3.1414926535)]
    [InlineData(0.08, 13, 3.218402766)]
    [InlineData(0.00012, 8334, 3.141472663)]
    [InlineData(0.00001, 100_001, 3.141602653)]
    public void Test(double epsilon, int countOfIteration, double expectedPi)
    {
        var (actualCount, actualPi) = PiApproximation.iterPi(epsilon);
        Assert.Equal((countOfIteration, (float) expectedPi), (actualCount,(float) actualPi));
    }
}