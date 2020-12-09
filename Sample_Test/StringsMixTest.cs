using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class StringsMixTest
    {
        [Theory]
        //[InlineData("my&friend&Paul has heavy hats! &", "my friend John has many many friends &", "2:nnnnn/1:aaaa/1:hhh/2:mmm/2:yyy/2:dd/2:ff/2:ii/2:rr/=:ee/=:ss")]
        //[InlineData("mmmmm m nnnnn y&friend&Paul has heavy hats! &", "my frie n d Joh n has ma n y ma n y frie n ds n&", "1:mmmmmm/=:nnnnnn/1:aaaa/1:hhh/2:yyy/2:dd/2:ff/2:ii/2:rr/=:ee/=:ss")]
        //[InlineData("Are the kids at home? aaaaa fffff", "Yes they are here! aaaaa fffff", "=:aaaaaa/2:eeeee/=:fffff/1:tt/2:rr/=:hh")]
        [InlineData("Yflog0epudFbqnjDjvfcBpsvv2cxum", "Xsyto-isyfKqwjw2fblr8fkxl8mosy", "1:vvv/2:fff/2:sss/2:yyy/1:cc/1:jj/1:pp/1:uu/2:ll/2:oo/2:ww")]
        public void MixTest(string input1, string input2, string expected)
        {
            Assert.Equal(expected, Kata.StringMix(input1, input2));
        }
    }
}