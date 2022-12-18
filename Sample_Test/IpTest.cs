using CodeWars;
using Xunit;

namespace Sample_Test;

public class IpTest
{
    [Theory]
    [InlineData("128.114.17.104", 2154959208)]
    [InlineData("0.0.0.0", 0)]
    [InlineData("128.32.10.1", 2149583361)]
    [InlineData("99.107.86.186", 1667978938)]
    public void Sample1Test(string expected, uint input)
    {
        Assert.Equal(expected, Kata.UInt32ToIP(input));
    }
}