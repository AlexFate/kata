using CodeWars;
using Xunit;

namespace Sample_Test;

public class LastDigitOfLargeTest
{
    [Theory]
    [InlineData("3715290469715693021198967285016729344580685479654510946723", "68819615221552997273737174557165657483427362207517952651", 7)]
    [InlineData("150", "53", 0)]
    [InlineData("4", "1", 4)]
    [InlineData("9", "7", 9)]
    [InlineData("10", "100", 0)]
    public void Test(string a, string b, int expected)
    {
        Assert.Equal(expected, Kata.LastDigit(a, b));
    }
}