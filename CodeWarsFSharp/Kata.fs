namespace CodeWarsFSharp
open System
open Microsoft.FSharp.Core

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

module MagnetParticularsInBoxes =
    let v (k:float) (n:float) = 1.0 / (k *  (n + 1.0) ** (2.0*k))
    let u k N = [|for n in 1 .. N -> v k (float n)|] |> Array.sum
    let S K N = [|for k in 1 .. K -> u (float k) N|] |> Array.sum
    let doubles maxK maxN = S maxK maxN

module PhoneDirectory =
    open System.Linq
    [<Struct>]
    type PhoneBookRow = {
        Phone: string
        Name: string
        Address: string
    }
    
    let toString (row: PhoneBookRow) =
        let del = ", "
        "Phone => " + row.Phone + del + "Name => " + row.Name + del + "Address => " + row.Address
    
    let getLines (line: string) =
        line.Split("\n" |> Seq.toArray)
    
    let search (num: string) (lines: string[]) =
        lines |> Array.Parallel.choose (fun (line: string) -> if line.Contains(num) then Some line else None)
        
    let extractName (line:string) =
        let initialIndex = line.IndexOf('<') + 1
        let endIndex = line.IndexOf('>') - 1
        line.[initialIndex..endIndex]
    
    let isAddress (chars: char seq) =
        let nonAddressSymbols = ['<'; '>'; '+';]
        let founded = chars |> Seq.tryFind (nonAddressSymbols.Contains)
        match founded with
        | Some _ -> false
        | _ -> true
    
    let extractAddress (line: string) =
        line.Split([|' '; ';'|]) |> Seq.filter (fun sub -> not (String.IsNullOrEmpty(sub)) ) |> Seq.filter (fun subString -> subString |> isAddress) |>
        Seq.map (fun sub -> sub.TrimStart().TrimEnd()) |> String.concat " "
    
    let replaceGarbage (address: string) =
        address.Replace("_", " ").Replace(",", "").Replace("/", "").TrimStart().TrimEnd()
    
    let toPhoneBookRow (num: string) (line: string) =
        let address = line |> extractAddress |> replaceGarbage
        {
            Phone = num
            Address = address
            Name = line |> extractName
        }
        
    let phone (input: string) (num: string) =
        let addressBooklines = input |> getLines |> search ("+" + num)
        match addressBooklines.Count() with
        | 0 -> "Error => Not found: " + num
        | 1 -> addressBooklines.[0] |> toPhoneBookRow num |> toString
        | _ -> "Error => Too many people: " + num

/// https://www.codewars.com/kata/550527b108b86f700000073f/train/fsharp
module PiApproximation =
    let inline p i b =
      (pown -1.0 i)/(double b)

    let inline f s v =
      let counter, value = s
      (counter + 1, value + v)

    let inline r v =
      let (counter, value) = v
      printf "%f" value
      counter, 4.0 * value

    let iterPi epsilon =
      [|for a in 3 .. 2 .. 10 -> a|] 
        |> Array.Parallel.mapi p
        |> (Array.fold f (0, 0.0))

/// https://www.codewars.com/kata/5552101f47fc5178b1000050
module PlayWithDigits =
    let stringifyInt num = num.ToString()
    let floatParse num = num |> fun i -> i.ToString() |> Convert.ToDouble
    let getNumIntegers num =
        num |> stringifyInt |> Seq.map floatParse
    let digPow num initialPow =
        let sumOfPowered = getNumIntegers num
                            |> Seq.mapi (fun i itm -> Math.Pow(itm, float (initialPow+i)))
                            |> Seq.sum
                            |> int
        match sumOfPowered % num = 0 with
         | true -> (sumOfPowered / num) |> int64
         | _ -> int64 -1
    let DigPow(num, initialPow) = digPow num initialPow 
