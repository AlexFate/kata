using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        private const int BiggestShipLength = 4;
        private const int AllShipsOnFieldPixelSum = 4 + 3 * 2 + 2 * 3 + 1 * 4;

        public static bool ValidateBattlefield(int[,] field)
        {
            return IsCorrectCountOfShipPixel(field) && IsCorrectShipCountLength(field) && HasNoShipOnDiagonal(field);
        }

        private static bool IsCorrectCountOfShipPixel(int[,] field)
        {
            return field.Cast<int>().Sum() == AllShipsOnFieldPixelSum;
        }
        
        private static bool IsCorrectShipCountLength(int[,] field)
        {
            var list = new List<string>();
            for (var i = 0; i < field.GetLength(1); i++)
            {
                var strings = string.Join("", GetColumn(field, i)).Split('0', StringSplitOptions.RemoveEmptyEntries).ToList();
                strings.AddRange(string.Join("", GetRow(field, i)).Split('0', StringSplitOptions.RemoveEmptyEntries));
                
                if (!strings.All(item => item.Length <= BiggestShipLength)) return false;
                list.AddRange(strings);
            }
            
            return IsCorrectCountOfAllShips(list.Select(item => item.Length));
        }

        private static bool IsCorrectCountOfAllShips(IEnumerable<int> ships)
        {
            return ships.Count(item => item == 4) <= 1 &&
                   ships.Count(item => item == 3) <= 2 &&
                   ships.Count(item => item == 2) <= 3;
        }
        
        private static IEnumerable<T> GetRow<T>(T[,] array, int row)
        {
            for (int i = 0; i <= array.GetUpperBound(1); ++i)
                yield return array[row, i];
        }
        
        private static IEnumerable<T> GetColumn<T>(T[,] array, int column)
        {
            for (var i = 0; i <= array.GetUpperBound(0); ++i)
                yield return array[i, column];
        }

        private static bool HasNoShipOnDiagonal(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0) - 1; i++)
            {
                var nextI = i + 1;
                for (int j = 0; j < field.GetLength(1) - 1; j++)
                {
                    var nextJ = j + 1;
                    if (field[i, j] == 1 && field[i, j] == field[nextI, nextJ] ||
                        field[nextI, j] == 1 && field[nextI, j] == field[i, nextJ]) 
                        return false;
                }
            }

            return true;
        }
    }
}