﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

namespace AdventOfCode

module Main = 
    open Day3

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
        
        do printfn "%i" totalOverlaps
        
        Seq.iter (fun i -> printfn "%i" i) nonoverlapIDs

        0 // return an integer exit code