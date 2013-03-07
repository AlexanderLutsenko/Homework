let listSum list = 
    let rec sum list a b currentIndex maxIndex maxValue = 
        match list with
        | hd::tl ->
            if (a+hd) > maxValue then
                let maxIndex = currentIndex-1
                let maxValue = a+hd
                sum tl b hd (currentIndex+1) maxIndex maxValue
            else sum tl b hd (currentIndex+1) maxIndex maxValue
        | [] -> maxIndex
    match list with 
    | h1::h2::tl -> sum tl h1 h2 3 0 0
    | _ -> 0

printfn "%A" (listSum [1; 5; 1; 2; 8; 13; 5; 10])