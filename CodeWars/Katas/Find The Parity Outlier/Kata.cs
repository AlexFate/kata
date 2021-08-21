using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static int Find(int[] integers)
        {
            var firstEven = integers.First(item => item % 2 == 0);
            var lastEven = integers.Last(item => item % 2 == 0);

            return (firstEven == lastEven) ? firstEven : integers.First(item => item % 2 != 0);
        }
    }
}