namespace AdventOfCode

module Day11 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq

    let fuel x y = 
        let bsc = (x + 10) * (x + 10) * y + 8772 * (x + 10)
        ((bsc % 1000) - (bsc % 100)) / 100 - 5

    let score (x : int) (y : int) (l : int) = 
        [x .. (x + l - 1)]
        |> List.sumBy
            (fun x -> 
                [y .. (y + l - 1)]
                |> List.sumBy 
                    (fun y -> fuel x y)
            )

    let mutable (bestScore : int) = Int32.MinValue
    let mutable (bestx : int) = -1
    let mutable (besty : int) = -1
    let mutable (bestl : int) = -1
    let mutable (sc : int) = 0
    
    let fuelarray = Array2D.init<int> 301 301 (fun x y -> fuel x y)  

    let getBestScore (u : unit) = 
        for x = 1 to 300 do
            Console.WriteLine("X:{0}", x)

            for y = 1 to 300 do
                sc <- fuelarray.[x,y]

                if sc > bestScore
                then 
                    bestScore <- sc
                    bestx <- x
                    besty <- y
                    bestl <- 0
            
                if Math.Max(x, y) < 300
                then
                    for l = 1 to 300 - Math.Max(x, y) do
                        // each increase in l adds a critical strip
                        // (x .. x + l - 1), y + l
                        // x + l, (y .. y + l - 1)
                        // x + l, y = l

                        for xt = x to x + l - 1 do
                            sc <- sc + fuelarray.[xt, y + l]

                        for yt = y to y + l - 1 do
                            sc <- sc + fuelarray.[x + l, yt]

                        sc <- sc + fuelarray.[x + l, y + l]

                        if sc > bestScore
                        then
                            bestScore <- sc
                            bestx <- x
                            besty <- y
                            bestl <- l
    
        (bestx, besty, bestl)