using System.Collections;
using System.Collections.Generic;
using CodeWarsFSharp;
using Xunit;

namespace Sample_Test
{
    public class MaximumSubArrayTest
    {

        [Theory]
        [ClassData(typeof(Input))]
        public void FSharpSolutionTest(List<int> input, List<int> expected)
        {
            Assert.Equal(expected, MaximumSubArraySum.getBiggerSub(input));
        }
        
        private class Input : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new List<int> {-2, 1, -3, 4, -1, 2, 1, -5, 4},
                    new List<int> {4, -1, 2, 1}, 
                },                
                new object[]
                {
                    new List<int> {2, 3, -4},
                    new List<int> {2, 3}, 
                },                
                new object[]
                {
                    new List<int> {2, 3, -4, 6},
                    new List<int> {2, 3, -4, 6}, 
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}