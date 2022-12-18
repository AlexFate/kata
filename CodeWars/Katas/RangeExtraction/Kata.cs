using System.Collections.Generic;
using System.Linq;

namespace CodeWars;

public partial class Kata 
{
    public static string Extract(int[] args)
    {
        if (!args.Any()) return "";
            
        static bool IsGap(int left, int right) => right - left > 1;
        var last = args.Length - 1;
        var start = 0;
        var ranges = new List<(int, int)>(args.Length);
        foreach(var current in Enumerable.Range(0, last)) 
        {
            var next = current + 1;
            if (IsGap(args[current], args[next])) 
            {
                ranges.Add((start, current));
                start = next;
            }
        }
        ranges.Add((start, last));

        var result = ranges.Select(tup => {
            var (st, end) = tup;
            var range = (end - st) switch {
                0 => args[end].ToString(),
                1 => $"{args[st]},{args[end]}",
                _ => $"{args[st]}-{args[end]}"
            };
            return range;
        });
        return string.Join(',', result);
    }
}