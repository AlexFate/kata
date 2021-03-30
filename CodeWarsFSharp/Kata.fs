namespace CodeWarsFSharp
open System
open System.Collections.Generic

module BasicSequencePractice =
    let rec sum list =
        match list with
        | head :: tail -> head + sum tail
        | [] -> 0
    let generateSequence index =
        let negativeReturn = (abs(index) / index)
        Seq.init (abs(index)+1) (fun i -> sum [0..1..i] * negativeReturn)
        
module StopSpinningMyWords =
    let rec spin (word: string) =
        word
        |> Seq.rev
        |> String.Concat
    let spinWords (text: string) =
        text.Split [|' '|]
        |> Seq.map (fun word ->
            match word.Length >= 5 with
            | true -> spin word
            | false -> word)
        |> Seq.toArray
        |> String.concat " "
        
module CategorizeNewMember =
    type MembershipType = Open | Senior
    let isSenior age handicap = age > 55 && handicap > 7
    let getMembership (pair: list<int>) =
        match isSenior pair.[0] pair.[1] with
        | true -> MembershipType.Senior.ToString()
        | _ -> MembershipType.Open.ToString()
    let OpenOrSenior = List.map getMembership
    let categorizeForTest listOfAgeHandicaps =
        listOfAgeHandicaps
        |> Seq.map List.ofSeq
        |> List.ofSeq
        |> OpenOrSenior
module CategorizeNewMemberSecondVariant =
   let (|Open|Senior|) [age; handicap] = if age > 55 && handicap > 7 then Senior else Open
   let OpenOrSenior =
        List.map <| fun pair ->
            match pair with
            | Senior -> "Senior"
            | Open -> "Open"
   let categorizeForTest listOfAgeHandicaps =
        listOfAgeHandicaps
        |> Seq.map List.ofSeq
        |> List.ofSeq
        |> OpenOrSenior
        
module MaximumSubArraySum =
    let parseTailOnSubs listOfNums=
        listOfNums
        |> List.mapi (fun i _ ->
            List.take (i + 1) listOfNums)
    let rec getAllSubs listOfNums =
        match listOfNums with
        | _ :: tail -> getAllSubs tail |> List.append (parseTailOnSubs tail)
        | [] -> []
        |> List.append (parseTailOnSubs listOfNums)
    
    let findBiggerSubsIndex listOfSubs=
        listOfSubs
        |> List.map (List.reduce (+))
        |> List.mapi (fun i num -> i, num)
        |> List.maxBy snd
        |> fst

    let getBiggerSub listOfNums =
       let nums = listOfNums |> List.ofSeq
       let subs = getAllSubs nums
       subs.[findBiggerSubsIndex subs]

//TODO: it
module ListFiltering =
    let filterNums : obj list -> int list = List.filter (fun (item: obj) -> item :? int ) >> List.map unbox
    
module Summation = let rec summation num = [0 .. num] |> List.sum

module Clock = let past h m s = (+) s >> (+) <| m * 60 >> (+) <| h * 3600 >> (*) 1000 <| 0

module ConsecutiveStrings =
    let longestConsec k (arr:seq<string>) =
        match k <= 0 with
        | true -> ""
        | false ->
            let arr = arr :?> list<string> 
            let mutable max = ""
            for i = 0 to (arr.Length - k) do
                let current = arr.[i .. (i + k - 1)] |> List.fold (+) ""
                if current.Length > max.Length then max <- current
            max
    let longestConsecAlt k (strings : string seq) =
        let n = Seq.length strings
        if 0 < k && k <= n then
            strings
            |> Seq.windowed k
            |> Seq.maxBy (Array.sumBy String.length)
            |> String.Concat
        else
            String.Empty
            
module BeginnerSeries3SumOfNumbers =
    let getSum a b =
        match a > b with
        | true -> [b .. a] |> List.sum
        | false -> [a .. b] |> List.sum
        
module SumOfParts =
    let partsSums (arr: int[]) =
        let sum = arr |> Array.sum
        let mutable difference = 0
        match arr with
        | [||] -> [|0|]
        | _ -> [|sum|] |> Array.append arr |> Array.map (fun item ->
                                                            let result = sum - difference
                                                            difference <- difference + item
                                                            result)
module HowMuch =
    let howMuch m n =
        [min m n .. max m n]
        |> List.filter (fun i -> (i % 7) = 2 && (i % 9) = 1)
        |> List.map (fun i -> ["M: " + (string)i; "B: " + (string)(i/7); "C: " + (string)(i/9)])