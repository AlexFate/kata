using CodeWars;
using Xunit;

namespace Sample_Test;

public class RangeExtractionTest
{
    [Theory]
    [InlineData(new[] {-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20}, "-6,-3-1,3-5,7-11,14,15,17-20")]
    [InlineData(new[] {10, 15}, "10,15")]
    [InlineData(new[] {10, 11, 12, 13, 15}, "10-13,15")]
    [InlineData(new int[] {}, "")]
    public void RangeExtraction(int[] input, string expected)
    {
        Assert.Equal(expected, Kata.Extract(input));
    }
}