let func1 x l = List.map (fun y -> y * x) l 

let func2 x = List.map ((*) x)

let func3 : int -> int list -> int list = List.map << (*)

printfn "%A" (func3 5 [1; 2; 3; 4; 5])