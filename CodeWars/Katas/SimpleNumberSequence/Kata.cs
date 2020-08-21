using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        #region SimpleNumberSequenceKata
        public static long SimpleNumber(string s)
        {
            var numbers = TryGetNumbersWithSymmetricChunks(s, out var isChunkFounded);

            if (!isChunkFounded)
            {
                numbers = TryGetNumbersWithFloatingChunks(s, out var isFloatingChunkFounded);
                if (!isFloatingChunkFounded) return -1;
            }
            
            return TryGetSkippedNumber(numbers);
        }

        private static long TryGetSkippedNumber(List<long> numbers)
        {
            var firstN = numbers.First();
            var lastN = numbers.Last();
            var arithmeticSum = (long)((firstN + lastN) * ((double)(lastN - firstN + 1) / 2));
            var currentSum = numbers.Sum();

            var answer = arithmeticSum - currentSum;

            return answer < lastN && answer > firstN ? answer : -1;
        }

        static List<long> TryGetNumbersWithSymmetricChunks(string input, out bool isChunkFounded)
        {
            try
            {
                var isXXXXXNumber = input.All(item => item.Equals(input.First()));
                if (input.Length == 1 || isXXXXXNumber)
                {
                    isChunkFounded = true;
                    return new List<long>() {Convert.ToInt64(input)};
                }

                var chunkSize = 1;
                isChunkFounded = false;
                var listNumbers = new List<long>();
                var longTypeMaxChunk = long.MaxValue.ToString().Length - 1;
                while (chunkSize <= input.Length / 2 + 1 && chunkSize < longTypeMaxChunk)
                {
                    listNumbers = Split(input, chunkSize).Select(item => Convert.ToInt64(item)).ToList();
                    listNumbers.Sort();
                    if (string.Join("", listNumbers).Equals(input))
                    {
                        if(listNumbers[1] - listNumbers[0] >= 9) break;
                        isChunkFounded = true;
                        break;
                    }
                    chunkSize++;
                }

                return listNumbers;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine();
                throw;
            }
        }
        static List<long> TryGetNumbersWithFloatingChunks(string input, out bool isNumsRestored)
        {
            if (input.Length == 3)
            {
                isNumsRestored = true;
                var firstNum = Convert.ToInt64(input.First().ToString());
                return new List<long>() {firstNum, Convert.ToInt64(string.Join("", input.TakeLast(2)))};
            }
            var indexOfFloat = input.IndexOf('0') - 1;

            var substringLessDigits = input.Substring(0, indexOfFloat);
            var substringMoreDigits = input.Substring(indexOfFloat);
            var lessNums = TryGetNumbersWithSymmetricChunks(substringLessDigits, out var isFoundLess);
            var moreNums = TryGetNumbersWithSymmetricChunks(substringMoreDigits, out var isFoundedMore);
            

            lessNums.AddRange(moreNums);
            lessNums.Sort();
            isNumsRestored = isFoundedMore && isFoundLess;
            return lessNums;
        }
        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        #endregion
    }
}