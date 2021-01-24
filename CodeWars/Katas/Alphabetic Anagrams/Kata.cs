using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        #region AlphabeticAnagrams

        public static long AlphabeticAnagramsListPosition(string input)
        {
            var sortedSet = input.OrderBy(ch => ch).ToList();
            return input.Aggregate<char, long>(1, (acc, ch) =>
            {
                var index = sortedSet.IndexOf(ch);
                sortedSet.RemoveAt(index);

                var charsAhead = sortedSet.Take(index).Distinct();

                var reducedValue = acc + charsAhead.Aggregate<char, long>(0, (a, letter) 
                    => a + CalculatePermutations(ReplaceFirst(string.Join("", sortedSet), letter, ch)));
                return reducedValue;
            });
        }

        public static long CalculatePermutations(string input)
        {
            var factorial = Factorial(input.Length);
            var charsCount = CountOfCharRepeatsInWord(input);

            var divider = charsCount.Aggregate<(char, int), long>(1, (acc, charCount) =>
            {
                var (_, count) = charCount;
                return acc * Factorial(count);
            });

            return factorial / divider;
        }
        
        private static IEnumerable<(char, int)> CountOfCharRepeatsInWord(string word)
        {
            var distinct = word.Distinct();
            return distinct.Select(ch => (ch, word.Count(item => item == ch))).ToList();
        }
        
        private static long Factorial(int n)
        {
            if (n == 0) return 1;

            long result = 1;
            for (var i = n; i > 0; i--)
            {
                result *= i;
            }

            return result;
        }

        private static string ReplaceFirst(string input, char oldValue, char newValue)
        {
            var index = input.IndexOf(oldValue);
            return input[.. index] + newValue + input[(index + 1) .. input.Length];
        }

        #endregion
    }
}