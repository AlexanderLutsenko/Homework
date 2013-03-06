let findIndex list x = 
    let rec fIndex list x acc =
        match list with
        |hd::tl -> 
            if hd = x then acc 
            else fIndex tl x (acc+1)    
        |[] -> 0
    fIndex list x 1

printfn "%A" (findIndex [0..10] 5)
        