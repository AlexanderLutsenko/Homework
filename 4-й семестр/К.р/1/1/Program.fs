let m list = List.collect (fun x -> [x; x]) list
printfn "%A" (m [1.; 2.; 3.])