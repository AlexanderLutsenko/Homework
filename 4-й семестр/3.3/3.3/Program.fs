let f1 list = List.map (fun x -> if x % 2 = 0 then 1 else 0) list |> List.sum 
let f2 list = List.filter (fun x -> x % 2 = 0) list |> List.length
let f3 list = List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0 list

let list = [5; 2; 3; 4; 8; 13; 19; 46]

printfn "%A"(f1 list)
printfn "%A"(f2 list)
printfn "%A"(f3 list)