using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static int MaxSequence(int[] input)
        {
            var allSubs = GetAllSubs(input);
            var indexedSums = allSubs
                .Select((item, index) => (index, item.Sum())).ToHashSet();

            var biggerValue = indexedSums
                .First(t => indexedSums.All(itm => itm.Item2 <= t.Item2));
            
            return biggerValue.Item2;
        }
        
        private static IEnumerable<IEnumerable<int>> ParseOnSubs(IEnumerable<int> input) 
            => input.Select((_, index) => input.Take(index+1));
        
        private static IEnumerable<IEnumerable<int>> GetAllSubs(IEnumerable<int> input)
        {
            var result = new List<List<int>>();
            var tail = input;
            while (true)
            {
                result = result.Union(new [] {tail}.Union(ParseOnSubs(tail))).Select(item => item.ToList()).ToList();
                tail = tail.Skip(1);
                if (!tail.Any()) break;
            }
            return result;
        }
    }
}