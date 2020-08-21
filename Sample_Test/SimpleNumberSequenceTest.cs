using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class SimpleNumberSequenceTest
    {
        [Theory]
        [InlineData("123567", 4)]
        [InlineData("899091939495", 92)]
        [InlineData("9899101102", 100)]
        [InlineData("599600601602", -1)]
        [InlineData("8990919395", -1)]
        [InlineData("1234567891012345678912",12345678911)]
        [InlineData("1011", -1)]
        [InlineData("1012", 11)]
        [InlineData("1013", -1)]
        [InlineData("910", -1)]
        [InlineData("912", -1)]
        [InlineData("911", 10)]
        [InlineData("4567891011121415", 13)]
        [InlineData("456789101112131415", -1)]
        [InlineData("99991000110002", 10000)]
        [InlineData("999899991000110002", 10000)]
        [InlineData("87726787726887726987727087727187727287727387727487727587727799939994999599969998999910000100011000210003", -1)]
        [InlineData("89908992", 8991)]
        [InlineData("684541684542684543684544684545684546684547684548684549684551684552684553", 684550)]
        [InlineData("994995996997998999100010021003", 1001)]
        public void Test(string input, long expected)
        {
            Assert.Equal(expected, Kata.SimpleNumber(input));
        }
    }
}