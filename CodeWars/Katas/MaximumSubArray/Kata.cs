using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        // public static List<int> GetBiggerSub()
        // {
        //     return new List<int>();
        // }
        //
        // private static IEnumerable<IEnumerable<int>> ParseOnSubs(IEnumerable<int> input) 
        //     => input.Select((_, index) => input.Take(index));
        //
        // private static IEnumerable<IEnumerable<int>> GetSubsWithOutFirst(IEnumerable<int> input)
        // {
        //     var head = input.FirstOrDefault();
        //     if (head != null)
        //     {
        //         var tail = input.Skip(1);
        //         return GetSubsWithOutFirst(tail).Union(ParseOnSubs(tail));
        //     }
        //     return 
        // }
        //
        // private static IEnumerable<IEnumerable<int>> GetAllSubs(IEnumerable<int> input) =>
        //     ParseOnSubs(input).Union(GetSubsWithOutFirst(input));

        // let rec allSubs listOfNums =
        //     match listOfNums with
        // | _ :: tail -> allSubs tail |> List.append (parseOnSubs tail)
        // | [] -> []
        // |> List.append (parseOnSubs listOfNums)
    }
}