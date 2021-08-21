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
            new[] { new [] {1, 4, 2, 3}, new [] {0, 1, 4, 4}, new [] {-1, 0, 1, 0}, new [] {2, 0, 4, 1}},
            new[] { new [] {1, 4, 2, 3, 5}, new [] {0, 1, 4, 4, 1}, new [] {-1, 0, 1, 0, 7}, new [] {2, 0, 4, 1, 0}, new [] {2, 1, -1, 7, 4}},
            new[] { new [] {2, 5, 3, 6, 3}, new [] {17,	5, 7, 4, 2}, new [] {7, 8, 5, 3, 2}, new [] {9, 4, -6, 8, 3}, new [] {2, -5, 7, 4, 2}}
        };

        private static readonly int[] Expected =
        {
            1, 
            -1, 
            -20, 
            65,
            2365,
            2060
        };

        [Fact]
        public void SampleTests()
        {
            for (int n = 5; n < Expected.Length; n++)
                Assert.Equal(Expected[n], (int)Kata.Determinant(Matrix[n]));
        }
    }
}