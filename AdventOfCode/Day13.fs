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

    let inJagged = 
        File.ReadAllLines("Day13input.txt")
        |> Array.map (fun s -> s.ToCharArray())

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

    let carts = 
        inJagged
        |> Array.mapi 
            (fun y ca -> 
                ca 
                |> Array.mapi 
                    (fun x c -> 
                        if c = '^' then Some(Bearing(x, y, North, Left, '|'))
                        elif c = 'v' then Some(Bearing(x, y, South, Left, '|'))
                        elif c = '<' then Some(Bearing(x, y, West, Left, '-'))
                        elif c = '>' then Some(Bearing(x, y, East, Left, '-'))
                        else None
                    )
                |> List.ofArray
                |> List.choose id
            )
        |> List.ofArray
        |> List.collect id

    exception Collision of int * int
    
    let colSet = new HashSet<(int * int)>()
    let wDict = new Dictionary<(int * int), char>()

    let doStepForCart (minecart : Cart) = 
        match minecart with
        | Bearing(x, y, H, I, w) ->
            if colSet.Contains((x,y))
            then 
                ignore (colSet.Remove((x,y)))
                None
            else
                let o = inJagged.[y].[x]
                let u1 = if y = 0 then ' ' else inJagged.[y - 1].[x]
                let d1 = if y = inJagged.Length - 1 then ' ' else inJagged.[y + 1].[x]
                let l1 = if x = 0 then ' ' else inJagged.[y].[x - 1]
                let r1 = if x = inJagged.[0].Length - 1 then ' ' else inJagged.[y].[x + 1]
            
                let (nx, ny, nH, nI, nw) = 
                    match H, w with
                    | North, '|' -> (x, y - 1, North, I, inJagged.[y - 1].[x])
                    | South, '|' -> (x, y + 1, South, I, inJagged.[y + 1].[x])
                    | East, '|' -> failwith "Illogical rail"
                    | West, '|' -> failwith "Illogical rail"
                    | North, '+' -> 
                        match I with
                        | Left -> (x - 1, y, West, Straight, inJagged.[y].[x - 1])
                        | Straight -> (x, y - 1, North, Right, inJagged.[y - 1].[x])
                        | Right -> (x + 1, y, East, Left, inJagged.[y].[x + 1])
                    | East, '+' ->
                        match I with
                        | Left -> (x, y - 1, North, Straight, inJagged.[y - 1].[x])
                        | Straight -> (x + 1, y, East, Right, inJagged.[y].[x + 1])
                        | Right -> (x, y + 1, South, Left, inJagged.[y + 1].[x])
                    | South, '+' ->
                        match I with
                        | Left -> (x + 1, y, East, Straight, inJagged.[y].[x + 1])
                        | Straight -> (x, y + 1, South, Right, inJagged.[y + 1].[x])
                        | Right -> (x - 1, y, West, Left, inJagged.[y].[x - 1])
                    | West, '+' ->
                        match I with
                        | Left -> (x, y + 1, South, Straight, inJagged.[y + 1].[x])
                        | Straight -> (x - 1, y, West, Right, inJagged.[y].[x - 1])
                        | Right -> (x, y - 1, North, Left, inJagged.[y - 1].[x])
                    | North, '/' -> (x + 1, y, East, I, inJagged.[y].[x + 1])
                    | East, '/' -> (x, y - 1, North, I, inJagged.[y - 1].[x])
                    | South, '/' -> (x - 1, y, West, I, inJagged.[y].[x - 1])
                    | West, '/' -> (x, y + 1, South, I, inJagged.[y + 1].[x])
                    | North, '\\' -> (x - 1, y, West, I, inJagged.[y].[x - 1])
                    | East, '\\' -> (x, y + 1, South, I, inJagged.[y + 1].[x])
                    | South, '\\' -> (x + 1, y, East, I, inJagged.[y].[x + 1])
                    | West, '\\' -> (x, y - 1, North, I, inJagged.[y - 1].[x])
                    | North, '-' -> failwith "Illogical rail"
                    | East, '-' -> (x + 1, y, East, I, inJagged.[y].[x + 1])
                    | South,'-' -> failwith "Illogical rail"
                    | West, '-' -> (x - 1, y, West, I, inJagged.[y].[x - 1])
                    | _, _ -> failwith "Unknown rail"

                if wDict.ContainsKey((x, y))
                then inJagged.[y].[x] <- wDict.Item((x,y))
                else inJagged.[y].[x] <- w

                let nc = inJagged.[ny].[nx]

                match nH with
                | North -> inJagged.[ny].[nx] <- '^'
                | East -> inJagged.[ny].[nx] <- '>'
                | South -> inJagged.[ny].[nx] <- 'v'
                | West -> inJagged.[ny].[nx] <- '<'
                
                if nc = '<' || nc = '>' || nc = '^' || nc = 'v' 
                then
                    Console.WriteLine("Collision {0},{1}", nx, ny)
                    ignore (colSet.Add((nx, ny)))
                    inJagged.[ny].[nx] <- wDict.Item((nx, ny))  // restore location to collision to previous content
                    None
                else
                    if wDict.ContainsKey((nx,ny)) 
                    then ()
                    else wDict.Add((nx, ny), inJagged.[ny].[nx])    
                    Some(Bearing(nx, ny, nH, nI, nw))

    let doStep (minecarts : Cart list) =
        match minecarts with
        | [] -> failwith "Exploded all carts"
        | x1 :: [] -> 
            match x1 with
            | Bearing(xo, yo,_,_,_) -> Console.WriteLine("last {0},{1}",xo,yo)
            
            let x1s = doStepForCart x1

            match x1s with
            | Some(Bearing(x,y,_,_,_)) -> raise(Collision(x,y))
            | None -> failwith "Unexpected collision"
        | x1 :: (x2 :: xs)-> 
            minecarts
            |> List.sortBy(function | Bearing(x, y, H, I, w) -> 151 * y + x)
            |> List.choose doStepForCart

    let runMine (n : int) = 
        [0 .. n]
        |> List.fold (fun mcl i -> doStep mcl) carts