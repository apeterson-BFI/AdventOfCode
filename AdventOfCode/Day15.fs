namespace AdventOfCode

module Day15 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    open System.Data
    open System.Globalization

    let inJagged = 
        File.ReadAllLines("Day15input.txt")

    type square = 
        | Wall
        | Empty
        | Goblin
        | Elf

    let toSquare (c : char) = 
        match c with
        | '#' -> Wall
        | '.' -> Empty
        | 'G' -> Goblin
        | 'E' -> Elf
        | _ -> failwith "Invalid character"

    let battleField = Array2D.init 32 32 (fun x y -> toSquare (inJagged.[y].[x]))

    let getEnemies (enemy : square) (bf : square [,]) = 
        let e = new List<(int * int)>()

        for x = 0 to 31 do
            for y = 0 to 31 do
                if bf.[x,y] = enemy then e.Add((x, y))

        e

