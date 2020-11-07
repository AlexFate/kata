namespace CodeWarsFSharp

module BasicSequencePractice =
    let rec sum list =
        match list with
        | head :: tail -> head + sum tail
        | [] -> 0
    let generateSequence index =
        let negativeReturn = (abs(index) / index)
        List.init (abs(index)+1) (fun i -> sum [0..1..i] * negativeReturn)