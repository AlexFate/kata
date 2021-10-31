using CodeWarsFSharp;
using Xunit;

namespace Sample_Test
{
    public class PhoneDirectoryTest
    {
        [Theory]
        [InlineData(
            "/+1-541-754-3010 156 Alphand_St. <J Steeve>\n" +
            "133, Green, Rd. <E Kustur> NY-56423 ;+1-541-914-3010!\n" +
            "<Anastasia> +48-421-674-8974 Via Quirinal Roma\n", 
            "1-541-754-3010", "Phone => 1-541-754-3010, Name => J Steeve, Address => 156 Alphand St.")]
        public void FoundCorrectLines(string input, string searchNumber, string expected)
        {
            Assert.Equal(expected, PhoneDirectory.phone(input, searchNumber));
        }

        [Theory]
        [InlineData("/+1-541-754-3010 156 Alphand_St. <J Steeve>", "J Steeve")]
        [InlineData("133, Green, Rd. <E Kustur> NY-56423 ;+1-541-914-3010!", "E Kustur")]
        [InlineData("<Anastasia> +48-421-674-8974 Via Quirinal Roma", "Anastasia")]
        public void ExtractName(string input, string expected)
        {
            Assert.Equal(expected, PhoneDirectory.extractName(input));
        }        
        
        [Theory]
        [InlineData("/+1-541-754-3010 156 Alphand_St. <J Steeve>", "156 Alphand_St.")]
        [InlineData("133, Green, Rd. <E Kustur> NY-56423 ;+1-541-914-3010!", "133, Green, Rd. NY-56423")]
        [InlineData("<Anastasia> +48-421-674-8974 Via Quirinal Roma", "Via Quirinal Roma")]
        public void ExtractAddress(string input, string expected)
        {
            Assert.Equal(expected, PhoneDirectory.extractAddress(input));
        }
    }
}