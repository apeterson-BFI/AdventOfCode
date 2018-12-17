namespace AdventOfCode

module Day14 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    open System.Security.AccessControl
    open System.Data
    open System.Globalization

    let inputDay14 = 440231

    let arrDay14 = [| 4;4;0;2;3;1 |]

    let digits2 (n : int) = 
        if n >= 10 
        then [n / 10; n % 10]
        else [n]

    let makeRecipes (elf1Index : int) (elf2Index : int) (recipes : List<int>) = 
        let elfscore1 = recipes.[elf1Index]
        let elfscore2 = recipes.[elf2Index]

        let total = elfscore1 + elfscore2

        if total < 10
        then 
            let newscore = total
            recipes.Add(newscore)
        else
            let newscore1 = total / 10
            let newscore2 = total % 10

            recipes.Add(newscore1)
            recipes.Add(newscore2)

        let newIndex1 = (elf1Index + 1 + elfscore1) % recipes.Count
        let newIndex2 = (elf2Index + 1 + elfscore2) % recipes.Count

        (newIndex1, newIndex2)

    let rec find10After (elf1Index : int) (elf2Index : int) (recipes : List<int>) (goal : int) = 
        if recipes.Count >= goal + 10
        then 
            [goal .. goal + 9]
            |> List.map (fun i -> recipes.[i])
        else
            let (e1, e2) = makeRecipes elf1Index elf2Index recipes
            find10After e1 e2 recipes goal

    let rec findNumBefore (elf1Index : int) (elf2Index : int) (recipes : List<int>) (target : int[]) = 
        if recipes.Count >= target.Length + 1
        then
            // each step creates 1 or 2 new entries
            // so this can only be the last or 2nd to last subsection
            // if recipes.Count = 20 and target.Length = 5
            // 15 16 17 18 19 is last
            // 14 15 16 17 18 is 2nd last
            // 
            // So start of 2nd last is recipes.Count - target.Length - 1
            // Start of last is recipes.Count - target.Length
            
            let f1 = 
                [0 .. (target.Length - 1)]
                |> List.map (fun i -> recipes.[recipes.Count - target.Length - 1 + i] = target.[i])
                |> List.reduce (fun b1 b2 -> b1 && b2)

            let f2 = 
                [0 .. (target.Length - 1)]
                |> List.map (fun i -> recipes.[recipes.Count - target.Length + i] = target.[i])
                |> List.reduce (fun b1 b2 -> b1 && b2)

            if f1 then recipes.Count - target.Length - 1
            elif f2 then recipes.Count - target.Length
            else
                let (e1, e2) = makeRecipes elf1Index elf2Index recipes
                findNumBefore e1 e2 recipes target
        elif recipes.Count = target.Length then
            let f1 = 
                [0 .. (target.Length - 1)]
                |> List.map (fun i -> recipes.[i] = target.[i])
                |> List.reduce (fun b1 b2 -> b1 && b2)

            if f1 then 0
            else
                let (e1, e2) = makeRecipes elf1Index elf2Index recipes
                findNumBefore e1 e2 recipes target
        else
            let (e1, e2) = makeRecipes elf1Index elf2Index recipes
            findNumBefore e1 e2 recipes target
            
    let startList (n : unit) = 
        let l = new List<int>()
        l.Add(3)
        l.Add(7)

        l

    let part1 = 
        let p1List = startList()
        find10After 0 1 p1List inputDay14

    let part2 = 
        let p2List = startList()

        findNumBefore 0 1 p2List arrDay14