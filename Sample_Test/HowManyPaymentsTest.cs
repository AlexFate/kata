using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CodeWars;

using Xunit;

namespace Sample_Test
{
    public class HowManyPaymentsTest
    {
        [Theory]
        [InlineData("z_logs.csv")]
        public async Task MainTest(string path)
        {
            var collections = await Kata.ReadFromCsv(path);
            var immutableHashSet = new HashSet<(string, string)>(collections);
            var memCapacity = immutableHashSet.Count / 2;
            
            var setOfSets = new HashSet<HashSet<(string, string)>>(memCapacity);
            
            foreach (var pair in immutableHashSet)
            {
                var firstFounded = setOfSets.FirstOrDefault(set => set.Any(tuple => tuple.PartialEqual(pair)));
                if (firstFounded != null)
                {
                    firstFounded.Add(pair);
                }
                else
                {
                    setOfSets.Add(new HashSet<(string, string)> {pair});
                }
            }

            while (true)
            {
                var setsToRemove = new HashSet<HashSet<(string, string)>>(memCapacity);
            
                foreach (var set in setOfSets)
                {
                    var firstWithSame = setOfSets
                        .LastOrDefault(item => item.PartialEqual(set));
            
                    if (firstWithSame != null && !firstWithSame.FullEqual(set))
                    {
                        firstWithSame.UnionWith(set);
                        setsToRemove.Add(set);
                    }
                }
            
                setOfSets = setOfSets.Except(setsToRemove).ToHashSet();
            
                if (!setsToRemove.Any()) break;
            }

            var maxCount = setOfSets.Select(item => item.Count).Max();
            Assert.Equal(63, maxCount);
            Assert.Equal(immutableHashSet.Count(), setOfSets.Select(item => item.Count).Sum());
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
}