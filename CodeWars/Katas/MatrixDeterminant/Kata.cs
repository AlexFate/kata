using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWars
{
    public partial class Kata
    {
        public static int Determinant(int[][] matrix)
        {
            return matrix.Length switch
            {
                1 => matrix[0][0],
                2 => matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1],
                3 => matrix[0][0] * matrix[1][1] * matrix[2][2] + 
                     matrix[0][1] * matrix[1][2] * matrix[2][0] +
                     matrix[0][2] * matrix[1][0] * matrix[2][1] -
                     matrix[2][0] * matrix[1][1] * matrix[0][2] -
                     matrix[2][1] * matrix[1][2] * matrix[0][0] - 
                     matrix[2][2] * matrix[1][0] * matrix[0][1],
                _ => new Func<int>(() =>
                {
                    var result = 0;
                    var matrixWithoutFirstRow = matrix[1 .. matrix.Length];
                    Parallel.For(0, matrix.Length,
                        () => 0,
                        (column, state, subResult) =>
                        {
                            var subArrays = matrixWithoutFirstRow.Select(item => item[.. column].Concat(item[(column + 1) .. item.Length]).ToArray());
                            return subResult += (int)Math.Pow(-1, column) * matrix[0][column] * Determinant(subArrays.ToArray());
                        },
                        (subResult) =>
                        {
                            result += subResult;
                        });
                    return result;
                }).Invoke()
            };
        }
    }
}