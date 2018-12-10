namespace AdventOfCode

module Day10 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    
    let inputRegex = new Regex(@"position=<([\s-]\d+),\s([\s-]\d+)> velocity=<([\s-]\d+),\s([\s-]\d+)>")

    let inLines = File.ReadAllLines("Day10input.txt")

    type particle = 
        { x : int; y : int; xv : int; yv : int }

    let getParticle (line : string) = 
        let m = inputRegex.Match(line)

        { x = parseInt m.Groups.[1].Value; y = parseInt m.Groups.[2].Value; xv = parseInt m.Groups.[3].Value; yv = parseInt m.Groups.[4].Value; }

    let atTime (p : particle) (t : int) = 
        (p.x + p.xv * t, p.y + p.yv * t)

    // What message will eventually appear in the sky?
    //
    // We have the parse the visual representation of text, appearing by this point font.

    let particles = 
        inLines
        |> Array.map getParticle
        
    