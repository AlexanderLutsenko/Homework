let supermap list func = List.collect (func) list
printfn "%A" (supermap [1.; 2.; 3.] (fun x -> [sin x; cos x]))