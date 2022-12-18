

using System;
using System.Linq;

namespace CodeWars;

public partial class Kata
{
    public static string StripComments(string commentLines, string[] commentSymbols)
    {
        var lines = commentLines.Split("\n");
        var result = lines.Select(line =>
        {
            if (!commentSymbols.Any(line.Contains)) return line.TrimEnd();
            if (commentSymbols.Any(line.StartsWith)) return "";
                
            var parts = line.Split(commentSymbols, StringSplitOptions.None);
            return parts.Any() ? parts[0].TrimEnd() : "";
        });
        return string.Join("\n", result);
    }
}