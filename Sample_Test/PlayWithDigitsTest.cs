using Xunit;
using CodeWarsFSharp;

namespace Sample_Test
{
    public class PlayWithDigitsTest
    {
        [Theory]
        [InlineData(46288, 5, -1)]
        [InlineData(46288, 3, 51)]
        [InlineData(92, 1, -1)]
        [InlineData(89, 1, 1)]
        public void Test(int inputNum, int initialPow, long expected)
        {
            Assert.Equal(expected, PlayWithDigits.DigPow(inputNum, initialPow));
        }
    }
}