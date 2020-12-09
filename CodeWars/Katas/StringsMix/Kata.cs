using System;
using System.Collections.Generic;
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

            var a = s1SortedSet.Union(s2SortedSet);
            
            var listRows = (from uniqueItem in s1SortedSet.Union(s2SortedSet).SkipWhile(item => item != 'a')
                let countIn1 = s1.Count(item => item == uniqueItem)
                let countIn2 = s2.Count(item => item == uniqueItem)
                select (countIn1 == countIn2) switch
                {
                    true => "=:" + new string(uniqueItem, countIn1),
                    false when (countIn1 < countIn2) => "2:" + new string(uniqueItem, countIn2),
                    _ => "1:" + new string(uniqueItem, countIn1)
                })
                .OrderByDescending(x => x.Length)
                .ThenBy(x => !x.Contains('=') ? string.Join("", x.Take(2)) : "9")
                .ThenBy(x => string.Join("", x.Skip(2)))
                .TakeWhile(item => item.Length > 3)
                .ToList();
            
            return string.Join('/', listRows);
        }
    }
}