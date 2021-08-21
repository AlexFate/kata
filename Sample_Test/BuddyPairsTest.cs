using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class BuddyPairsTest
    {
        [Theory]
        [InlineData(10, 50, "(48 75)")]
        [InlineData(48, 50, "(48 75)")]
        [InlineData(310, 2755, "(1050 1925)")]
        [InlineData(8983, 13355, "(9504 20735)")]
        [InlineData(1071625, 1103735, "(1081184 1331967)")]
        [InlineData(14656, 20372, "Nothing")]
        [InlineData(2177, 4357, "Nothing")]
        [InlineData(2382, 3679, "Nothing")]
        public void Test(int start, int limit, string expected)
        {
            Assert.Equal(expected, Kata.Buddy(start, limit));
        }
    }
}