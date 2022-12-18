using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWars;

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

    public static async Task<int> BiggerPayments(string pathToLogsCsv)
    {
        var collections = await ReadFromCsv(pathToLogsCsv);

        var setOfSets = FirstCompressIteration(ref collections);

        CompressingProcess(ref setOfSets);
            
        return setOfSets.Select(item => item.Count).Max();
    }

    public static async Task<IEnumerable<(string, string)>> ReadFromCsv(string path)
    {
        if(!File.Exists(path))
            throw new FileNotFoundException("Csv file is not exist.", path);

        return (await File.ReadAllLinesAsync(path))
            .Select(item => item.Split(","))
            .Select(item => (item[0], item[1]));
    }
    private static IEnumerable<HashSet<(string, string)>> FirstCompressIteration(
        ref IEnumerable<(string, string)> input)
    {
        var setOfSets = new HashSet<HashSet<(string, string)>>();
            
        foreach (var pair in input)
        {
            var firstFounded = setOfSets.FirstOrDefault(set => set.Any(tuple => tuple.PartialEqual(pair)));
            if (firstFounded != null)
            {
                firstFounded.Add(pair);
            }
            else
            {
                setOfSets.Add(new HashSet<(string, string)> {pair});
            }
        }

        return setOfSets;
    }

    private static void CompressingProcess(ref IEnumerable<HashSet<(string, string)>> setOfSets)
    {
        while (true)
        {
            var setsToRemove = new HashSet<HashSet<(string, string)>>();
            foreach (var set in setOfSets)
            {
                var firstWithSame = setOfSets.FirstOrDefault(item => item.PartialEqual(set));

                if (firstWithSame.FullEqual(set)) continue;

                firstWithSame?.UnionWith(set);
                setsToRemove.Add(set);
            }

            setOfSets = setOfSets.Except(setsToRemove).ToHashSet();

            if (!setsToRemove.Any()) return;
        }
    }
    #endregion
}

internal static class HowManyPaymentKataExtensions
{

    internal static bool PartialEqual(this HashSet<(string, string)> hashSet1, HashSet<(string, string)> hashSet2)
    {
        foreach (var item in hashSet1)
        {
            foreach (var item2 in hashSet2)
            {
                if (item.PartialEqual(item2))
                {
                    return true;
                }
            }
        }
            
        return false;
    }

    internal static bool FullEqual(this ICollection<(string, string)> hashSet1, ICollection<(string, string)> hashSet2)
    {
        if (hashSet1 == null) return hashSet2 == null;
        if (hashSet1.Count != hashSet2.Count) return false;
            
        return !hashSet1.Except(hashSet2).Any();
    }
        
    internal static bool PartialEqual(this (string, string) item1, (string, string) item2)
    {
        var (email, tel) = item1;
        var (email2, tel2) = item2;
            
        return email == email2 || tel == tel2;
    }
}