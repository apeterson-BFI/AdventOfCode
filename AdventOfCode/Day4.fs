namespace AdventOfCode

module Day4 =
    open Day1
    open Day2
    open Day3
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    
    // We want:
    // The guard who has the most minutes slept
    // The minute they slept the most.
    // 
    // They are asleep on the minute they fall asleep, awake includes the minute they wake up.

    // guard #num begins shift - sets that Guard ID as the context for all events that follow.
    //
    //
    // Steps:
    // 
    // 1 - Parse input
    // 3 - Process input
    // 4 - Conclusions
    //

    type GuardEvent = 
    | StartShift of System.DateTime * int
    | Sleep of System.DateTime
    | Wake of System.DateTime

    // PARSING
    // Message types -
    // [YYYY-MM-DD HH:MM] falls asleep
    // [YYYY-MM-DD HH:MM] wakes up
    // [YYYY-MM-DD HH:MM] Guard #(num) begins shift

    let rawEvents = File.ReadAllLines("Day4input.txt")

    let sleepPattern = new Regex(@"\[(\d\d\d\d-\d\d-\d\d \d\d:\d\d)\] falls asleep");
    let wakePattern = new Regex(@"\[(\d\d\d\d-\d\d-\d\d \d\d:\d\d)\] wakes up");
    let shiftPattern = new Regex(@"\[(\d\d\d\d-\d\d-\d\d \d\d:\d\d)\] Guard #(\d+) begins shift");

    let eventToDT (ge : GuardEvent) = 
        match ge with
        | StartShift(dt, ident) -> dt
        | Sleep(dt) -> dt
        | Wake(dt) -> dt

    let parseLogDate (logStamp : string) = 
        System.DateTime.ParseExact(logStamp, "yyyy-MM-dd HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo)

    let toEvent (line : string) = 
        let shiftMatch = shiftPattern.Match(line)
        let sleepMatch = sleepPattern.Match(line)
        let wakeMatch = wakePattern.Match(line)

        if shiftMatch.Success then StartShift(parseLogDate shiftMatch.Groups.[1].Value, parseInt shiftMatch.Groups.[2].Value)
        elif sleepMatch.Success then Sleep(parseLogDate sleepMatch.Groups.[1].Value)
        elif wakeMatch.Success then Wake(parseLogDate wakeMatch.Groups.[1].Value)
        else failwith "Unmatched Guard Event"

    let orderedEvents = 
        rawEvents
        |> List.ofArray
        |> List.map toEvent
        |> List.sortBy eventToDT

    // PROCESS
    // We need to keep track of Guard, Minute pairs for sleeping
    // Keep track of the active Guard on Duty, and whether he is currently sleeping.

    // Sequential processing
    // if Current State is Guard#, Awake then do nothing, but update current state
    // if Current State is Guard#, Asleep, then from Sleep Minute to Wake Minute - 1, add to the sleep bank.
    // if Guard shift start, set Guard#, Awake

    // Reduce

    type SleepTracker = 
        {
            GuardID : int;
            Minute : int;
        }

    type GuardState = 
    | Awake of int * System.DateTime * SleepTracker list
    | Asleep of int * System.DateTime * SleepTracker list

    let guardFold (gs : GuardState) (ge : GuardEvent) = 
        match gs with
        | Awake(id, dt, stl) ->
            match ge with
            | StartShift(shdt, gid) -> Awake(gid, shdt, stl)
            | Sleep(sldt) -> Asleep(id, sldt, stl)
            | Wake(wkdt) -> failwith "Do not expect waking up from Awake. Wakeception"
        | Asleep(id, dt, stl) ->
            match ge with
            | StartShift(shdt, gid) -> failwith "Do not expect next shft without Waking first"
            | Sleep(sldt) -> failwith "Do not expect sleeping from Asleep. Sleepception"
            | Wake(wkdt) ->
                let newstl = 
                    [dt.Minute .. wkdt.Minute - 1]
                    |> List.map (fun i -> { GuardID = id; Minute = i; })

                Awake(id, wkdt, stl @ newstl)

    let finalGuardState =
        List.fold guardFold (Awake(0, System.DateTime.MinValue, [])) orderedEvents

    let sleepTrackers = 
        match finalGuardState with
        | Awake(_, _, stl) -> stl
        | Asleep(_, _, _) -> failwith "Unexpected sleeping at end"

    let guardMinutes = 
        sleepTrackers
        |> Seq.ofList
        |> Seq.countBy (function | { GuardID = gid; Minute = m;} -> gid)
        |> Seq.sortBy snd
        |> Enumerable.Last

    let sleepyGuard = fst guardMinutes
    
    let sleepyGuardMinute = 
        sleepTrackers
        |> List.filter (fun st -> st.GuardID = sleepyGuard)
        |> Seq.ofList
        |> Seq.countBy (function | {GuardID = gid; Minute = m;} -> m)
        |> Seq.sortBy snd
        |> Enumerable.Last
        |> fst
        
    let guardMinute = sleepyGuard * sleepyGuardMinute

    let sleepiestGuardMinute = 
        sleepTrackers
        |> Seq.ofList
        |> Seq.countBy (function | { GuardID = gid; Minute = m; } -> (gid, m))
        |> Seq.sortBy snd
        |> Enumerable.Last
        |> fst

    let (sGuard, sMinute) = sleepiestGuardMinute

    let sGuardMinute = sGuard * sMinute