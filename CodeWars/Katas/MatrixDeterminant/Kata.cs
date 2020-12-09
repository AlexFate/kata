using System;
using System.Linq;

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
                _ => GetDeterminant(matrix)
            };
        }
        
        private static int GetDeterminant(int[][] matrix)
        {
            var result = 0;
            var matrixWithoutFirstRow = matrix[1 .. matrix.Length];
            for (var column = 0; column < matrix.Length; column++)
            {
                int n = matrixWithoutFirstRow.Length;
                int[][] subArrays = new int[n][];
                for (int i = 0; i < n; i++)
                {
                    subArrays[i] = matrixWithoutFirstRow[i][.. column].Concat(matrixWithoutFirstRow[i][(column + 1) ..(n + 1)]).ToArray();
                }
                result += (int) Math.Pow(-1, column) * matrix[0][column] * Determinant(subArrays.ToArray());
            }
            return result;
        }
    }
}