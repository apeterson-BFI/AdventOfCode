namespace AdventOfCode

module Day12 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq

    let initialStateRegex = new Regex(@"initial state: ([#\.]+)")
    let ruleRegex = new Regex(@"([#\.]+) => ([#\.]+)")

    let inLines = File.ReadAllLines("Day12input.txt")

    let initialStateMatch = initialStateRegex.Match(inLines.[0])
    
    let initialStateText = 
        initialStateMatch
        |> (fun m -> m.Groups.[1].Value)

    let rulesText = 
        inLines.Skip(1)
        |> Seq.map (fun s -> ruleRegex.Match(s))
        |> Seq.map (fun m -> (m.Groups.[1].Value, m.Groups.[2].Value))
        |> List.ofSeq

    let rulesRegexes = 
        rulesText
        |> List.map (fun (s1, s2) -> (new Regex(s1.Replace(".", @"\.")), s2))

    let doRuleMatch (state : string) (sb : StringBuilder) (ruleR : Regex, res : string) = 
        let matches = 
            (ruleR.Matches(state))

        for i = 0 to matches.Count - 1  do
            sb.Chars(matches.Item(i).Index + 2) <- res.Chars(0)

        sb

    let doGen (rules : (Regex * string) list) (begIndex : int, state : string) = 
        let s1 = "....." + state + "....."
        let sb = new StringBuilder(new String('.', s1.Length))

        let rm = doRuleMatch s1

        let sbfinal = List.fold rm sb rules
                
        let sfinal = sbfinal.ToString()

        let newBegIndex = begIndex - 5

        // from fst to lst is: fst, lst - fst + 1
        (newBegIndex, sfinal)
        
    let rec runNGens (pots : int * string) (n : int) (i : int) = 
        if i = n
        then pots
        else 
            let rv = doGen rulesRegexes pots
            runNGens rv n (i + 1)
 

    let g (i : int) = 
        let (resBeg, resTxt) = runNGens (0, initialStateText) i 0
        
        Console.WriteLine()
        Console.WriteLine(resBeg)
        Console.WriteLine(resTxt)

        let mutable score = 0

        for i = 0 to resTxt.Length - 1 do
            if resTxt.Chars(i) = '#'
            then score <- score + (resBeg + i)

        score