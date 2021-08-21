using System;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static int[] TwoSum(int[] numbers, int target)
        {
            var map = numbers.ToHashSet();
            for (int i = 0; i < map.Count; i++)
            {
                var difference = target - numbers[i];
                if (!map.Contains(difference)) continue;
                
                var j = Array.IndexOf(numbers, difference);
                if (i == j) continue;
                
                return new[] { j, i };
            }
            return Array.Empty<int>();
        }
    }
}