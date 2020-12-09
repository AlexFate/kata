using System.Collections.Immutable;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static string StringMix(string s1, string s2)
        {
            var s1SortedSet = s1.ToImmutableSortedSet();
            var s2SortedSet = s2.ToImmutableSortedSet();

            var listRows = (from uniqueChar in s1SortedSet.Union(s2SortedSet).SkipWhile(item => item < 'a' || item < 'A')
                let countIn1 = s1.Count(item => item == uniqueChar)
                let countIn2 = s2.Count(item => item == uniqueChar)
                select (countIn1 == countIn2) switch
                {
                    true => "=:" + new string(uniqueChar, countIn1),
                    false when (countIn1 < countIn2) => "2:" + new string(uniqueChar, countIn2),
                    _ => "1:" + new string(uniqueChar, countIn1)
                })
                .OrderByDescending(x => x.Length)
                .ThenBy(SortByStringNums)
                .ThenBy(SortByAlphabet)
                .TakeWhile(SkipLengthThatLessOrEqualOne);
            
            return string.Join('/', listRows);
        }

        private static string SortByStringNums(string x)
        {
            const string makeEqualSymbolLast = "9";
            return !x.Contains('=') ? string.Join("", x.Take(2)) : makeEqualSymbolLast;
        }

        private static string SortByAlphabet(string x) => string.Join("", x.Skip(2));

        private static bool SkipLengthThatLessOrEqualOne(string item) => item.Length > 3;
    }
}