using CodeWarsFSharp;
using Xunit;

namespace Sample_Test;

public class StopSpinningMyWordsTest
{
    [Theory]
    [InlineData("Hey fellow warriors", "Hey wollef sroirraw")]
    [InlineData("This is a test", "This is a test" )]
    [InlineData("This is another test", "This is rehtona test")]
    public void Test(string input, string expected)
    {
        Assert.Equal(expected, StopSpinningMyWords.spinWords(input));
    }
}