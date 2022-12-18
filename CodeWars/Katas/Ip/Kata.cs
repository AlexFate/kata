using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeWars;

public partial class Kata
{
    #region IpKata
    public static string UInt32ToIP(uint ip)
    {
        if (ip == 0) return "0.0.0.0";
        var bits = GetValid32bitsString(Convert.ToString(ip, 2));
        var ipBytes = Regex.Matches(bits, ".{8}")
            .Select(item => Convert.ToByte(item.Groups[0].Value, 2))
            .ToArray();
        return string.Join(".", ipBytes);
    }
    private static string GetValid32bitsString(string current)
    {
        return current.Length == 32 
            ? current 
            : current.Insert(0, new string('0', 32 - current.Length));
    }
    #endregion
}