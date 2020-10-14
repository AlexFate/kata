using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        #region ArrayDiffKata

        public static int[] ArrayDiff(int[] a, int[] b)
        {
            var bSet = new HashSet<int>(b);
            return a.Where(v => !bSet.Contains(v)).ToArray();
        }
        #endregion
    }
}