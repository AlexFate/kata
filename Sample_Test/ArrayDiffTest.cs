using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class ArrayDiffTest
    {
        [Fact]
        public void Sample1Test()
        {
            Assert.Equal(new int[] {2},       Kata.ArrayDiff(new int[] {1, 2},    new int[] {1}));
            Assert.Equal(new int[] {2, 2},    Kata.ArrayDiff(new int[] {1, 2, 2}, new int[] {1}));
            Assert.Equal(new int[] {1},       Kata.ArrayDiff(new int[] {1, 2, 2}, new int[] {2}));
            Assert.Equal(new int[] {1, 2, 2}, Kata.ArrayDiff(new int[] {1, 2, 2}, new int[] {}));
            Assert.Equal(new int[] {},        Kata.ArrayDiff(new int[] {},        new int[] {1, 2}));
        }
    }
}