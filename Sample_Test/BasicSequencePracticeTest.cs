using System.Collections.Generic;
using CodeWarsFSharp;
using Xunit;

namespace Sample_Test
{
    public class BasicSequencePracticeTest
    {
        [Theory]
        [InlineData(5, 0, 1, 3, 6, 10, 15)]
        [InlineData(-5 , 0, -1, -3, -6, -10, -15)]
        [InlineData(7, 0,  1,  3,  6,  10,  15,  21,  28)]
        public void Test(int index, params int[] expected)
        {
            var result = BasicSequencePractice.generateSequence(index);
            Assert.Equal(expected, result);
        }
    }
}