namespace CodeWarsFSharp
open System

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
    open System
    let filterNums : list<Object> -> seq<Object> = 
        Seq.filter (fun (item: Object) -> item :? int )

    filterNums [1;2;3;'a'; 5; 't']