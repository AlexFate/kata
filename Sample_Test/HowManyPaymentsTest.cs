using System.Linq;
using System.Threading.Tasks;
using CodeWars;
using Xunit;

namespace Sample_Test;

public class HowManyPaymentsTest
{
    [Theory]
    [InlineData("z_logs.csv")]
    public async Task MainTest(string path)
    {
        Assert.Equal(63, await Kata.BiggerPayments(path));
    }
        
    [Theory]
    [InlineData("z_logs.csv")]
    public async Task ReadingFromCsvToDictionaryTest(string path)
    {
        var collection = await Kata.ReadFromCsv(path);
            
        Assert.True(collection.Any());
        Assert.True(collection.Count() == 6083);
    }
}