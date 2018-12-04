namespace AdventOfCode

module Day2 =
    open Day1
    open System.IO
    open Utils

    let lines = File.ReadAllLines("Day2input.txt")

    let rec lineReader (m : Map<char, int>) (l : char list) =
        match l with
        | [] -> m
        | x :: xs -> 
            match m.TryFind(x) with
            | Some(i) -> lineReader (m.Add(x, i + 1)) xs
            | None -> lineReader (m.Add(x, 1)) xs

    let lineScorer (m : Map<char, int>) = 
        let li =
            m
            |> Map.toList
            |> List.map snd
        
        let two = List.exists (fun i -> i = 2) li
        let three = List.exists (fun i -> i = 3) li

        ((if two then 1 else 0), (if three then 1 else 0))

    let charlists = 
        lines
        |> List.ofArray
        |> List.map (fun m -> Seq.toList m)

    let (twos, threes) = 
        charlists
        |> List.map (lineReader (Map.empty<char, int>))
        |> List.map lineScorer
        |> List.reduce (fun (tw1, th1) (tw2, th2) -> (tw1 + tw2, th1 + th2))
     
    let res1 = twos * threes

    let rec commontext (s1 : char list) (s2 : char list) (r : char list) = 
        match s1, s2 with
        | [], [] -> r
        | [], x2 :: xs2 -> failwith "Strings should be same length"
        | x1 :: xs1, [] -> failwith "Strings should be same length"
        | x1 :: xs1, x2 :: xs2 -> if x1 = x2 then commontext xs1 xs2 (List.append r [x1]) else commontext xs1 xs2 r
        
    let commons = List.collect (fun cl1 -> List.map (fun cl2 -> commontext cl1 cl2 []) charlists) charlists

    let commonButOne = List.filter (fun cl -> List.length cl = 25) commons

    let res2 = 
        commonButOne
        |> List.map (fun cl -> new System.String(Array.ofList cl))

    