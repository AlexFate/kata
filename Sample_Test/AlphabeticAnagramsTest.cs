using System.Collections.Generic;
using Xunit;
using CodeWars;

namespace Sample_Test
{
    public class AlphabeticAnagramsTest
    {
        [Theory]
        [InlineData("OLOLOLOFP", 1503)]
        [InlineData("ABAB", 2)]
        [InlineData("QUESTION", 24572)]
        [InlineData("BOOKKEEPER", 10743)]
        [InlineData("MUCHOCOMBINATIONS", 1938852339039)]
        public void CalculateSortedPermutations(string input, long expected)
        {
            Assert.Equal(expected, Kata.AlphabeticAnagramsListPosition(input));
        }
        
        [Theory]
        [InlineData("OLOLOLOFP", 2520)]
        [InlineData("MUCHOCOMBINATIONS", 3705077376000)]
        public void CalculatePermutationsTest(string input, long expected)
        {
            Assert.Equal(expected, Kata.CalculatePermutations(input));
        }
    }
}