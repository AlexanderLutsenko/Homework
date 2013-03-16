let createList x = 
    let rec crList x acclist accnumber = 
        if x <= 0 then acclist
        else crList (x - 1) (acclist @ [accnumber * 2]) (accnumber * 2)
    crList x [] 1

printfn "%A" (createList 5)
