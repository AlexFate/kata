using System.Collections;
using System.Collections.Generic;
using CodeWarsFSharp;
using Xunit;

namespace Sample_Test;

public class CategorizeNewMemberTest
{
    [Theory]
    [ClassData(typeof(Input))]
    public void Test(List<List<int>> input, List<string> expected)
    {
        Assert.Equal(expected, CategorizeNewMember.categorizeForTest(input));
    }        
    [Theory]
    [ClassData(typeof(Input))]
    public void SecondVariantTest(List<List<int>> input, List<string> expected)
    {
        Assert.Equal(expected, CategorizeNewMemberSecondVariant.categorizeForTest(input));
    }
        
    private class Input : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[]
            {
                new List<List<int>>
                {
                    new() {18, 20},
                    new() {45, 2},
                    new() {61, 12},
                    new() {37, 6},
                    new() {21, 21},
                    new() {78, 9},
                },
                new List<string>
                {
                    "Open", "Open", "Senior", "Open", "Open", "Senior"
                }
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}