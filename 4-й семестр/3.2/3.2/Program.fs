let checkList list = 
    let sortedList = List.sort list
    let rec check list = 
        match list with
        | h1::h2::tl -> 
            if h1 = h2 then false
            else check (h2::tl)
        | _ -> true
    check sortedList
        

printfn "%A" (checkList [1; 5; -1; 2; 8; 13; 10])