﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

namespace AdventOfCode

module Main = 
    open Day13

    [<EntryPoint>]
    let main argv = 
        
        // Day 1
        //let freq = endingFreq
        //printfn "%i" freq
    
        //let rfreq = repFreq
        //printfn "%i" repFreq

        //printfn "%i" res1
        
        //let r2 = List.length res2

        //List.iter (fun s -> printfn "%s" s) res2
        
//        do printfn "%i" totalOverlaps
//        
//        Seq.iter (fun i -> printfn "%i" i) nonoverlapIDs

//        do printfn "Part 1 Guard %i" sleepyGuard
//        do printfn "Part 1 Minute %i" sleepyGuardMinute
//        do printfn "Part 1 Times %i" guardMinute 
//        do printfn "Part 2 Guard %i" sGuard
//        do printfn "Part 2 Minute %i" sMinute
//        do printfn "Part 2 Times %i" sGuardMinute

        //do printfn "%i" (product2.Length)
        //do printfn "%i" (minL)
        //do printfn "%i" score
        //do printfn "%i" closeRegionSize
        
        let mcRes = 
            try
                ignore (runMine 40000)
                ()
            with
            | Collision (x, y) -> 
                printfn "%i, %i" x y

        
        0 // return an integer exit code