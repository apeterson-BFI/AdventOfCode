namespace AdventOfCode

module Day6 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq

    let rawCoordinates = File.ReadAllLines("Day6input.txt")

    let coordRegex = new Regex(@"(\d+), (\d+)")

    type Coordinates = int * int

    let toCoordinates (raw : string) = 
        let m = coordRegex.Match(raw)
        let mv = (m.Groups.[1].Value, m.Groups.[2].Value)
        
        match mv with
        | Int32 i1, Int32 i2 -> (i1, i2)
        | _ -> failwith "Non-numeric coordinate input"

    let coordinates =
        rawCoordinates
        |> List.ofArray
        |> List.map toCoordinates

    // Find the largest region that's not infinite.

    // Create a bounding rectangle of the region of all points
    // Any region that has a point outside of the rectangle is infinite.
    // Proof:
    // Since P(x,y) is beyond all R(x,y) in both x and y,
    // and the point currently belongs to Ri, the score for Ri is lower than the score for R.
    // If we pick a point one more distant in x or y, depending on where the point is,
    // Our region's distance increases by one, but also all other regions distances increase by one.
    // In any case, if we are moving beyond all other regions in a dimension, leader remains the same.
    //
    // We can process this bounding rectangle instead.
    // We can process one bigger on each side of each dimension, making this extra row infinite?
    // if your region doesn't have any points on the 1st strip outside of the bounding region.
    // Any point further out would belong to one of the regions on that strip, or to no one, by an extension of the argument for the bounding box.

    let rectMinX = 
        coordinates
        |> List.map fst
        |> List.sort
        |> Enumerable.First
        

    let rectMinY = 
        coordinates
        |> List.map snd
        |> List.sort
        |> Enumerable.First
        

    let rectMaxX = 
        coordinates
        |> List.map fst
        |> List.sort
        |> Enumerable.Last
        

    let rectMaxY = 
        coordinates
        |> List.map snd
        |> List.sort
        |> Enumerable.Last

    let indexedCoordinates = 
        coordinates
        |> List.mapi  (fun i c -> (i, (fst c), (snd c)))

    let manhattanDistance (cindex : int, cx : int, cy : int) (x : int) (y : int) =
         Math.Abs(cx - x) + Math.Abs(cy - y)

    let xrange = [rectMinX - 10 .. rectMaxX + 10]
    let yrange = [rectMinY - 10 .. rectMaxY + 10]

    let points = 
        xrange
        |> List.collect 
            (fun x -> 
                yrange
                |> List.collect
                    (fun y ->
                        indexedCoordinates
                        |> List.map (fun ic -> (ic, manhattanDistance ic x y))
                        |> List.sortBy snd
                        |> (function | x1 :: (x2 :: xs) -> (if (x1 |> snd) < (x2 |> snd) then [x1] else []) | _ -> failwith "Less than 2 coords found")
                        |> List.map (fun ((index : int, cx : int, cy : int), d : int) -> (index, x, y))
                    )
            )
        
    let regions = 
        points
        |> Seq.groupBy (fun (index, x, y) -> index)
        |> Seq.filter (fun (index, grp) -> not (Seq.exists (fun (index, x, y) -> x = rectMinX - 10 || x = rectMaxX + 10 || y = rectMinY - 10 || y = rectMaxY + 10 ) grp))
    
    let scores = 
        regions
        |> Seq.map (fun (index, s) -> s.Count())

    let score = 
        scores
        |> Seq.max

    let closeRegionSize =
        xrange
        |> List.sumBy 
            (fun x -> 
                yrange
                |> List.filter 
                    (fun y ->
                        indexedCoordinates
                        |> List.map (fun ic -> (ic, manhattanDistance ic x y))
                        |> List.sumBy (fun (ic, d) -> d)
                        |> (fun t -> t < 10000)
                    )
                |> List.length
            )

