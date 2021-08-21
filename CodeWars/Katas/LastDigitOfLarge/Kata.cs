using System;
using System.Collections.Generic;

namespace CodeWars
{
    public partial class Kata
    {
        public static int LastDigit(string a, string b)
        {
            if (IsNumberZero(b) || IsNumberZero(a) && IsNumberZero(b)) return 1; 
            
            if (IsNumberZero(a)) return 0;
            
            var remainder = GetOnFourRemainder(b);
            
            var exp = remainder == 0 ? 4 : remainder; 
            
            var result = (int)Math.Pow(a[^1] - '0', exp); 
            
            return result % 10;
            
            //Or this answer
            //return (int)BigInteger.ModPow(BigInteger.Parse(a), BigInteger.Parse(b), 10);
        }
        private static int GetOnFourRemainder(IEnumerable<char> b)
        {
            var result = 0;
            foreach (var c in b) 
                result = (result * 10 + (c - '0')) % 4;
            return result;
        }

        private static bool IsNumberZero(string input) => input.Length == 1 && input[0] == '0';
    }
}