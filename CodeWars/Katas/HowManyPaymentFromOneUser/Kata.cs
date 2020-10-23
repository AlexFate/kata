using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWars
{
    public partial class Kata
    {
        // Покупая билет в кино, пользователь сервиса может указать свой email или телефон, либо email и телефон сразу.
        // Напишите алгоритм, который по списку таких транзакций находит пользователя, купившего больше всех билетов.
        // В качестве ответа укажите число строк в исходном файле, которые относятся к этому пользователю.
        // Под пользователем понимается email, телефон или комбинация обоих идентификаторов, если их удастся связать
        // по логам покупок.
        //
        // Пример связывания пользователей по логам:
        //
        // user_1@contest.yandex.ru, 880111111111
        //
        // user_1@contest.yandex.ru, 880222222222
        //
        // user_2@contest.yandex.ru, 880222222222
        //
        // user_3@contest.yandex.ru, 880333333333
        //
        // В этом примере адреса user_1@contest.yandex.ru, user_2@contest.yandex.ru
        // и телефоны 880111111111, 880222222222 относятся к одному пользователю.
        // А user_3@contest.yandex.ru и 880333333333 — к другому.
        #region HowManyPaymentsFromOneUser

        public static int BiggerPayments(string pathToLogsCsv)
        {
            return 0;
        }

        public static async Task<IEnumerable<(string, string)>> ReadFromCsv(string path)
        {
            if(!File.Exists(path))
                throw new FileNotFoundException("Csv file is not exist.", path);

            return (await File.ReadAllLinesAsync(path))
                .Select(item => item.Split(","))
                .Select(item => (item[0], item[1]));
        }
        #endregion
    }

    public static class HowManyPaymentKataExtensions
    {

        public static bool PartialEqual(this HashSet<(string, string)> hashSet1, HashSet<(string, string)> hashSet2)
        {
            var boo = false;

            foreach (var item in hashSet1)
            {
                foreach (var item2 in hashSet2)
                {
                    if (item.PartialEqual(item2))
                    {
                        boo = true;
                        break;
                    }
                }
                if(boo) break;
            }
            
            return boo;
        }

        public static bool FullEqual(this HashSet<(string, string)> hashSet1, HashSet<(string, string)> hashSet2)
        {
            if (hashSet1 == null || hashSet2 == null) return false;
            
            return !hashSet1.Except(hashSet2).Any();
        }
        
        public static bool PartialEqual(this (string, string) item1, (string, string) item2)
        {
            if (item1.Item1 == item2.Item1 || item1.Item2 == item2.Item2 || item1.Item1 == item2.Item2 ||
                item1.Item2 == item2.Item1)
                return true;
            return false;
        }
    }
}