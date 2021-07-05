using System.Linq;
using System.Threading.Tasks;
using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class TwoSumTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(new [] { 0, 1 }, Kata.TwoSum(new [] { 2, 2, 3 }, 4).OrderBy(a => a).ToArray());
            Assert.Equal(new [] { 0, 2 }, Kata.TwoSum(new [] { 1, 2, 3 }, 4).OrderBy(a => a).ToArray());
            Assert.Equal(new [] { 1, 2 }, Kata.TwoSum(new [] { 1234, 5678, 9012 }, 14690).OrderBy(a => a).ToArray());
        }
    }
}