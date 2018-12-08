namespace AdventOfCode

module Day8 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    open Day6

    let rawText = File.ReadAllText("Day8input.txt")

    let testText = File.ReadAllText("Day8test.txt")

    let toData (s : string) = 
        match s with
        | Int32 i -> i
        | _ -> failwith "Unrecognized non-numeric data"

    let rawData =         
        rawText.Split(' ')
        |> List.ofArray
        |> List.map toData

    let testData =
        testText.Split(' ')
        |> List.ofArray
        |> List.map toData

    let rd = new Stack<int>(rawData |> List.rev)
    let td = new Stack<int>(testData |> List.rev)

    // State / Parse
    //
    // [NODE]
    // Node_#Children
    // Node_#Metadata
    //
    // followed by Child Nodes
    // followed by Metadata
    //
    // State: # of Children Left to Read, # of Metadata left to read.

    type LicMeta = 
    | Metadata of int

    type LicNode = 
    | Node of int * int * (LicNode list) * (LicMeta list)

    let rec readNodeP1 (data : Stack<int>) = 
        let d = data.Pop()
        readNodeP2 data d

    and readNodeP2 (data : Stack<int>) (nodesToRead : int) =
        let d = data.Pop()
        readNodeP3 data nodesToRead d []

    and readNodeP3 (data : Stack<int>) (nodesToRead : int) (metadataToRead : int) (subNodes : LicNode list) = 
        if (List.length subNodes) = nodesToRead
        then readNodeP4 data nodesToRead metadataToRead subNodes []
        else readNodeP3 data nodesToRead metadataToRead (subNodes @ [readNodeP1 data])

    and readNodeP4 (data : Stack<int>) (nodesToRead : int) (metadataToRead : int) (subNodes : LicNode list) (subMetas : LicMeta list) : LicNode = 
        if (List.length subMetas) = metadataToRead
        then Node(nodesToRead, metadataToRead, subNodes, subMetas)
        else 
            let d = data.Pop()
            
            readNodeP4 data nodesToRead metadataToRead subNodes (subMetas @ [Metadata(d)])
                            
    let masterNode =
        readNodeP1 rd

    let testNode =
        readNodeP1 td 

    let rec totalOfMetadata (node : LicNode) = 
        match node with
        | Node(nodes, metadatas, nl, ml) -> 
            let directMetadata =
                ml |> List.sumBy (function | Metadata(i) -> i)

            let indirectMetadata = 
                nl
                |> List.sumBy totalOfMetadata

            directMetadata + indirectMetadata

    let mTotal = totalOfMetadata masterNode

    let tmTotal = totalOfMetadata testNode

    let rec valueNode (node : LicNode) = 
        match node with
        | Node(nodes, metadatas, nl, ml) ->
            match nl with
            | [] -> List.sumBy (function | Metadata(i) -> i) ml
            | x :: xs ->
                let mlook (m : LicMeta) =
                    match m with
                    | Metadata(i) ->
                        if i <= nodes 
                        then                            
                            List.item (i - 1) nl
                            |> valueNode
                        else
                            0

                ml
                |> List.sumBy mlook

    let mValue = valueNode masterNode
    let tmValue = valueNode testNode