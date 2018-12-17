namespace AdventOfCode

module Day13 =
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

    let inLines = File.ReadAllLines("Day13input.txt")
                  |> Array.map (fun l -> l.ToCharArray())

    let trackSize = 150

    let track = 
        Array2D.init trackSize trackSize (fun x y -> inLines.[y].[x])

    let underlay = Array2D.copy track

    // process carts top to bottom, left to right
    // cart turns left 1st time, straight 2nd time, right third time, then left 4th time and so on

    type Heading = 
    | North
    | East
    | South
    | West
    
    type Intersection = 
    | Left
    | Straight
    | Right

    type Cart = 
    | Bearing of int * int * Heading * Intersection * char

    let setupCart (x : int) (y : int) (c : char) = 
        if c = '^' then 
            underlay.[x, y] <- '|'
            Some(Bearing(x, y, North, Left, '|'))
        elif c = 'v' then 
            underlay.[x, y] <- '|'
            Some(Bearing(x, y, South, Left, '|'))
        elif c = '<' then 
            underlay.[x, y] <- '-'
            Some(Bearing(x, y, West, Left, '-'))
        elif c = '>' then 
            underlay.[x, y] <- '-'
            Some(Bearing(x, y, East, Left, '-'))
        else None        

    let setupRow (y : int) (ca : char[]) = 
        ca
        |> List.ofArray
        |> List.mapi (setupCart y)
        |> List.choose id

    let carts = 
        track
        |> Array2D.mapi setupCart
        |> Seq.cast<Cart option>
        |> Seq.toList
        |> List.choose id

    let takeIntersection (heading : Heading) (inter : Intersection) = 
        match heading, inter with
        | North, Left -> (West, Straight)
        | North, Straight -> (North, Right)
        | North, Right -> (East, Left)
        | East, Left -> (North, Straight)
        | East, Straight -> (East, Right)
        | East, Right -> (South, Left)
        | South, Left -> (East, Straight)
        | South, Straight -> (South, Right)
        | South, Right -> (West, Left)
        | West, Left -> (South, Straight)
        | West, Straight -> (West, Right)
        | West, Right -> (North, Left)

    let goDirection (x : int) (y : int) (H : Heading) = 
        match H with
        | North -> (x, y - 1)
        | East -> (x + 1, y)
        | South -> (x, y + 1)
        | West -> (x - 1, y)

    let goFwdSlashTurn (x : int) (y : int) (H : Heading) = 
        let h2 = 
            match H with
            | North -> East
            | East -> North
            | South -> West
            | West -> South

        let (x2, y2) = goDirection x y h2

        (x2, y2, h2)

    let goBackSlashTurn (x : int) (y : int) (H : Heading) = 
        //  \\
        let h2 = 
            match H with
            | North -> West
            | West -> North
            | South -> East
            | East -> South

        let (x2, y2) = goDirection x y h2

        (x2, y2, h2)

    let updateCartStatus (x : int) (y : int) (H : Heading) (I : Intersection) (w : char) = 
        match H, w with
        | _, '|' ->
            let (x2, y2) = goDirection x y H
            (x2, y2, H, I, track.[x2, y2])
        | _, '-' ->
            let (x2, y2) = goDirection x y H
            (x2, y2, H, I, track.[x2, y2])
        | _, '+' ->
            let (h2, i2) = takeIntersection H I
            let (x2, y2) = goDirection x y h2
            (x2, y2, h2, i2, track.[x2, y2])
        | _, '/' ->
            let (x2, y2, h2) = goFwdSlashTurn x y H
            (x2, y2, h2, I, track.[x2, y2])
        | _, '\\' ->
            let (x2, y2, h2) = goBackSlashTurn x y H
            (x2, y2, h2, I, track.[x2, y2])
        | _, _ -> failwith "Unknown rail"

    let moveCart (x : int) (y : int) (nx : int) (ny : int) (H : Heading) = 
        track.[x, y] <- underlay.[x, y]

        match H with
        | North -> track.[nx, ny] <- '^'
        | East -> track.[nx, ny] <- '>'
        | South -> track.[nx, ny] <- 'v'
        | West -> track.[nx, ny] <- '<'

    let clearCarts (x : int) (y : int) (cs : Cart list) (completed : Cart list) = 
        printfn "Collision %i,%i" x y
        
        track.[x, y] <- underlay.[x, y]
        
        let cs2 = 
            cs
            |> List.filter (function Bearing(x2,y2,_,_,_) -> not (x2 = x && y2 = y))

        let completed2 = 
            completed
            |> List.filter (function Bearing(x2,y2,_,_,_) -> not (x2 = x && y2 = y))
        
        (cs2, completed2)

    let rec doTick (toDo : Cart list) (completed : Cart list) = 
        match toDo with
        | [] -> completed
        | c :: cs -> 
            match c with
            | Bearing(x, y, H, I, w) ->
                let (nx, ny, nH, nI, nw) = updateCartStatus x y H I w
                let nc = track.[nx, ny]

                moveCart x y nx ny nH

                if nc = '<' || nc = '>' || nc = '^' || nc = 'v' 
                then 
                    let (cs2, completed2) = clearCarts nx ny cs completed

                    doTick cs2 completed2
                else
                    doTick cs (completed @ [Bearing(nx, ny, nH, nI, nw)])

    let rec runTicks (minecarts : Cart list) = 
        match minecarts with
        | [] -> failwith "No carts left"
        | c :: [] ->
            match c with
            | Bearing(x1, y1, _, _, _) ->
                printfn "One Cart - BS %i,%i" x1 y1

            let ft = doTick minecarts []

            match ft with
            | [] -> failwith "No carts left"
            | c2 :: [] ->
                match c2 with
                | Bearing(x, y, _, _, _) ->
                    printfn "One Cart - AS %i,%i" x y
            | _ -> failwith "Impossible"
        | _ ->
            let sortcarts = 
                minecarts
                |> List.sortBy(function | Bearing(x, y, _, _, _) -> 151 * y + x)
            
            runTicks (doTick sortcarts [])