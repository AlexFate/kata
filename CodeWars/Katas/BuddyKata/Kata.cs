using System;

namespace CodeWars;

public partial class Kata
{
    #region BuddyKata

    public static string Buddy(long start, long limit)
    {
        for (var i = start; i < limit; i++)
        {
            var potentialPairNum = GetPotentialPairNumber(i);
            var potentialNumberPotentialPair = GetPotentialPairNumber(potentialPairNum);
            if (i == potentialNumberPotentialPair && i < potentialPairNum)
                return $"({i} {potentialPairNum})";
        }
        return "Nothing";
    }
    private static long GetPotentialPairNumber(long n)
    {
        long result = 1;
        for (var i = 2; i <= Math.Sqrt(n); i++)
        {
            if (n % i != 0) continue;
            if (n / i == i)
            {
                result += i;
            }
            else
            {
                result += i;
                result += n / i;
            }
        }
        return result - 1;
    }
    #endregion
}