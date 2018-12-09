namespace AdventOfCode

module Day9 =
    open System
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    
    let inputRegex = new Regex(@"(\d+) players; last marble is worth (\d+) points")

    let inText = File.ReadAllText("Day9input.txt")

    let inMatch = inputRegex.Match(inText)

    let (players, lastMarblePoints) = (inMatch.Groups.[1].Value, inMatch.Groups.[2].Value)

//First, the marble numbered 0 is placed in the circle. At this point, while it contains only a single marble, 
// it is still a circle: the marble is both clockwise from itself and counter-clockwise from itself. 
// This marble is designated the current marble.

//Then, each Elf takes a turn placing the lowest-numbered remaining marble into the circle between the marbles that are 1 and 2 marbles clockwise of the current marble. 
// (When the circle is large enough, this means that there is one marble between the marble that was just placed and the current marble.) 
// The marble that was just placed then becomes the current marble
//However, if the marble that is about to be placed has a number which is a multiple of 23, 
// something entirely different happens. First, the current player keeps the marble they would have placed, adding it to their score. 
// In addition, the marble 7 marbles counter-clockwise from the current marble is removed from the circle and also added to the current player's score. 
// The marble located immediately clockwise of the marble that was removed becomes the new current marble.


// Place each marble not divisible by 23: between the marble one spot clockwise and the marble two spots clockwise of the active marble. The new marble becomes the current marble

// For marbles divisible by 23.
// Keep the marble that would have been placed, adding it to players score.
// Remove the marble 7 marbles COUNTER-clockwise from the current marble.
// The marble clockwise of the removed marble becomes the new marble.

// We need to keep playing until the last marble gives the indicated score.

// Circular data structure
// C# List?

    let circle = new System.Collections.Generic.List<int>()
    do circle.Add(0)

    // Test case: 10 players; last marble is worth 1618 points: high score is 8317

    let addMarble (currentIndex : int) (marble : int) = 
        let rm = modulo (currentIndex + 2) (circle.Count)

        if rm = 0 
        then 
            circle.Add(marble)   // if its between the last index and 0, it should be at the end of the list
            circle.Count - 1
        else 
            circle.Insert(rm, marble)      // Otherwise insert at the 2nd locations to put in between the two.
            rm

    let playMarbles (numPlayers : int) (lastMarble : int) = 
        do circle.Clear()
        do circle.Add(0)

        let players = Array.zeroCreate(numPlayers)

        let mutable activePlayer = 0
        let mutable activeIndex = 0

        for marble = 1 to lastMarble do
            if marble % 23 = 0
            then
                players.[activePlayer] <- players.[activePlayer] + marble
                
                let remIndex = modulo (activeIndex - 7) (circle.Count)
                
                if remIndex = circle.Count - 1
                then activeIndex <- 0
                else activeIndex <- remIndex

                players.[activePlayer] <- players.[activePlayer] + circle.[remIndex]
                do circle.RemoveAt(remIndex)
            else
                let na = addMarble activeIndex marble
                activeIndex <- na

            activePlayer <- (modulo (activePlayer + 1) (numPlayers))

        players.Max()