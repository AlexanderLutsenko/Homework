let findIndex list x = 
    let rec fIndex list x acc =
        match list with
        | hd :: tl -> 
            if hd = x then Some acc 
            else fIndex tl x (acc + 1)    
        | [] -> None
    fIndex list x 0

printfn "%A" (findIndex [0 .. 10] 5)
