let rotate list = 
    List.fold (fun acc elem -> elem :: acc) [] list

printfn "%A" (rotate [1 .. 100])
