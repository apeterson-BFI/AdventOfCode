namespace AdventOfCode

module Day1 =
    open System.Linq
    open System.Data
    open System.Collections.Generic
    open NodaTime
    open System.IO
    open Utils

    let freqChangesLines fn = 
        File.ReadAllLines(fn)

    let getChange (s : string) = 
        if s = "" then 0L
        else
            match s with
            | Int64 i -> i
            | _ -> failwith "Unrecognized line"

    let arrFreqs = 
        "Day1input.txt"
        |> freqChangesLines
        |> Array.map getChange
        
    // Answer 1
    let endingFreq = 
        arrFreqs
        |> Array.sum
    
    let len = arrFreqs.Length

    let rec getFirstRepeater (hs : HashSet<int64>) (index : int) (freq : int64) =
        if index >= len then getFirstRepeater hs (index - len) freq
        else
            let v = freq + arrFreqs.[index]

            if hs.Contains(v) then v
            else
                hs.Add(v)
                getFirstRepeater hs (index + 1) v

    let hs = new HashSet<int64>()
    hs.Add(0L)

    let repFreq = getFirstRepeater hs 0 0L