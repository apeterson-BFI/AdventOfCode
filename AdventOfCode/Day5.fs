namespace AdventOfCode

module Day5 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq

    let rawUnits = 
        File.ReadAllText("Day5input.txt")

    let reactTest (c1 : char) (c2 : char) = 
        Char.ToLower(c1) = Char.ToLower(c2) && c1 <> c2
 
    let cList = List.ofArray (rawUnits.ToCharArray())

    let rec localReact (revPriors : char list) (successors : char list) = 
        match revPriors, successors with
        | [], [] -> new String(Array.ofList [])
        | [], sx :: sxs ->
            match sxs with
            | [] -> new String(Array.ofList [sx])
            | sx2 :: sxs2 ->
                // sx, sx2 :: sxs2
                if (reactTest sx sx2)
                then localReact [] sxs2
                else 
                    // sx, sx2 is not a match
                    // sx2 is part of the successors, sx part of the priors
                    localReact [sx] sxs
        | px :: psx, [] ->
            // We have priors in rev order, but no successors. Everything has been processed.
            
            let rList = 
                revPriors
                |> List.rev

            new String(Array.ofList rList)
        | px :: psx, sx :: sxs ->
            // px is the left reactant, sx the right reactant
            if (reactTest px sx)
            then localReact psx sxs
            else
                // px and sx are not reacting
                // slide over 1.
                // put the new rightmost element of revPriors in the leftmost spot.
                localReact (sx :: revPriors) sxs

    let product2 = localReact [] cList

    let alphabetRegexes = 
        List.map2 (fun c1 c2 -> new Regex(sprintf "%s|%s" (string c1) (string c2))) [ 'a' .. 'z' ] [ 'A' .. 'Z' ]
    
    let reductions = 
        alphabetRegexes
        |> List.map (fun r -> r.Replace(rawUnits, ""))
        |> List.map (fun poly -> localReact [] (List.ofArray (poly.ToCharArray())))
        |> List.map (fun poly -> poly.Length)
        |> List.sort

    let minL = 
        match reductions with
        | x :: xs -> x
        | [] -> failwith "No reductions exist."
        
        
