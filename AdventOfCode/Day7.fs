namespace AdventOfCode

module Day7 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq

    let rawLines = File.ReadAllLines("Day7input.txt")

    let stepPattern = new Regex(@"Step ([A-Z]) must be finished before step ([A-Z]) can begin.")

    type StepOrder =
        char * char

    let stepOrders = 
        rawLines
        |> List.ofArray
        |> List.map (fun l -> stepPattern.Match(l))
        |> List.map (fun m -> (m.Groups.[1].Value, m.Groups.[2].Value))
        |> List.map (fun (s1 : string, s2 : string) -> (s1.Chars 0, s2.Chars 0))

    // Only C is available, and so it is done first.
    // Next, both A and F are available. A is first alphabetically, so it is done next.
    // Then, even though F was available earlier, steps B and D are now also available, and B is the first alphabetically of the three.
    // After that, only D and F are available. E is not available because only some of its prerequisites are complete. Therefore, D is completed next.
    // F is the only choice, so it is done next.
    // Finally, E is completed.

    // So, Steps can have multiple prerequisites
    // at each point in the process, we have to consider the steps available to be done now, and pick the alphabetically earliest.

    // build list of Letters required for each Letter
    let letterRequirements = 
        ['A' .. 'Z']
        |> List.map (fun c1 -> (c1, stepOrders |> List.filter (fun (cr : char, cb : char) -> c1 = cb) |> List.map (fun (cr, cb) -> cr)))

    // When we complete a Letter, we can remove that letter from all requirements lists.

    let rec processLetters (letterOrder : char list) (letterReqs : (char * char list) list) = 
        match letterReqs with
        | [] -> letterOrder
        | x :: xs ->        
            let (fc1 : char, fclist : char list) = 
                letterReqs
                |> List.find (fun (c1 : char, clist : char list) -> (List.length clist) = 0)

            let newLR = 
                letterReqs
                |> List.filter (fun (c1 : char, _) -> c1 <> fc1)
                |> List.map (fun (c1, clist : char list) -> (c1, clist |> List.filter (fun (c2 : char) -> c2 <> fc1)))

            let newLO = letterOrder @ [fc1]

            processLetters newLO newLR

    let letterOrder = new String(Array.ofList (processLetters [] letterRequirements))

    // 4 Elves can work on the letters simulataneously.
    // They should still take on the letters in alphabetical order, among whats available.
    // It takes 60 seconds + the Letter value (A = 1) to finish a task.
    // How long will it take to finish.
    // The turn they start counts as one second.

    // So if a working has 3 turns to go, and they start on 0 they finish on 2, and can pick up a new job on 3.

    // If the last turn anyone was working was 14, 15 is the number of seconds answer.

    type Elf = 
    | Idle
    | Working of char * int

    let doWork (e : Elf) =  
        match e with
        | Idle -> Idle
        | Working(c, 1) -> Working(c,0)
        | Working(c, i) -> Working(c, i - 1)

    let receiveWorkers (elist: Elf list) = 
        let receiveWorker (e : Elf) =
            match e with
            | Idle -> (None, e)
            | Working(c, 0) -> (Some(c), Idle)
            | Working(_,_) -> (None, e) 

        let receiving =
            elist
            |> List.map receiveWorker

        let chars = 
            receiving
            |> List.choose fst

        let elves = 
            receiving
            |> List.map snd

        (chars, elves) 
        
    let isIdle (e : Elf) = 
        match e with
        | Idle -> true
        | Working(_,_) -> false

    let allIdle (elist : Elf list) = 
        elist 
        |> List.forall isIdle

    let letterCost (c : char) = 
        int c - int 'A' + 61

    let rec processWorkers (letterOrder : char list) (letterReqs : (char * char list) list) (workers : Elf list) (second : int) = 
        match letterReqs with
        | [] -> 
            let (chlist : char list, elves : Elf list) = 
                receiveWorkers workers
            
            if (allIdle elves)
            then second
            else
                let newLR = 
                    letterReqs
                    |> List.map (fun (c1, clist : char list) -> (c1, clist |> List.filter (fun c2 -> not (chlist.Contains(c2))))) 

                processWorkers (letterOrder @ chlist) newLR (elves |> List.map doWork) (second + 1)
        | x :: xs ->
            let (chlist : char list, elves : Elf list) = 
                receiveWorkers workers

            let n1letterReqs = 
                letterReqs
                |> List.map (fun (c1, clist : char list) -> (c1, clist |> List.filter (fun c2 -> not (chlist.Contains(c2)))))

            let flist = 
                n1letterReqs
                |> List.filter (fun (c1 : char, clist : char list) -> (List.length clist) = 0)

            let idleElves = 
                elves
                |> List.filter isIdle
            
            let busyElves = 
                elves
                |> List.filter (fun e-> not (isIdle e))

            let takeLength = 
                Math.Min(flist.Length, idleElves.Length)

            let assignLetters = 
                flist.Take takeLength
                |> List.ofSeq

            let pickupElves = 
                List.map (fun l -> Working(fst l, letterCost (fst l))) assignLetters

            let stillIdleElves = 
                let stillIdle = 5 - (List.length busyElves) - (List.length pickupElves)

                List.replicate stillIdle Idle

            let updElves = 
                busyElves @ pickupElves @ stillIdleElves

            let lettersToRemove = 
                assignLetters
                |> List.map fst

            let upLetterReqs = 
                n1letterReqs
                |> List.filter (fun (c1 : char, _) -> not (lettersToRemove.Contains(c1)))
                
            processWorkers (letterOrder @ lettersToRemove) upLetterReqs (updElves |> List.map doWork) (second + 1)

    let finishTime = 
        processWorkers [] letterRequirements (List.replicate 5 Idle) 0