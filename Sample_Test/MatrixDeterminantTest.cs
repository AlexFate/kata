using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class MatrixDeterminantTest
    {
        private static readonly int[][][] Matrix =
        {
            new[] { new [] { 1 } },
            new[] { new [] { 1, 3 }, new [] { 2, 5 } },
            new[] { new [] { 2, 5, 3 }, new [] { 1, -2, -1 }, new [] { 1, 3, 4 } },
            new[] { new [] {1, 4, 2, 3}, new [] {0, 1, 4, 4}, new [] {-1, 0, 1, 0}, new [] {2, 0, 4, 1}}
        };

        private static readonly int[] Expected =
        {
            1, 
            -1, 
            -20, 
            65
        };

        [Fact]
        public void SampleTests()
        {
            for (int n = 0; n < Expected.Length; n++)
                Assert.Equal(Expected[n], (int)Kata.Determinant(Matrix[n]));
        }

        // [Theory]
        // [InlineData(0, 1, 2, 3, 4, 5)]
        // public void TakeWithoutIndexTest(int index, params int[] input)
        // {
        //     Assert.Equal(new int[] {2, 3, 4, 5}, Kata.TakePartWithoutIndex(input, index));
        // }
    }
}