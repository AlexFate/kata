using System.Linq;
using System.Threading.Tasks;
using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class StripCommentsTest
    {
        [Fact]
        public async Task Test()
        {
            Assert.Equal("apples, pears\ngrapes\nbananas", 
                Kata.StripComments("apples, pears # and bananas\ngrapes\nbananas !apples", new [] { "#", "!" }));
            Assert.Equal("", Kata.StripComments("a", new [] { "a" }));
            Assert.Equal("", Kata.StripComments("# some text", new [] { "#" }));
        }
    }
}