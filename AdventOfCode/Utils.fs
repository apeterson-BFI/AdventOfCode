namespace AdventOfCode

module Utils = 
    
    let (|Int64|_|) str =
       match System.Int64.TryParse(str) with
       | (true,i) -> Some(i)
       | _ -> None

    let (|Int32|_|) str =
       match System.Int32.TryParse(str) with
       | (true,i) -> Some(i)
       | _ -> None
   
    let parseInt s = 
        match s with
        | Int32 i -> i
        | _ -> failwith "Text is not an Int32"

    let modulo n m = ((n % m) + m) % m