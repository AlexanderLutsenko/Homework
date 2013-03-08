exception OperatorError of string

type Tree =
    | Number of float
    | Operation of char * Tree * Tree

let solveTree tree = 
    let rec sTree tree =
        match tree with
        | Number(value) -> value
        | Operation(operator, left, right) ->
            match operator with
            | '+' -> sTree(left) + sTree(right)  
            | '-' -> sTree(left) - sTree(right)  
            | '*' -> sTree(left) * sTree(right)  
            | '/' -> sTree(left) / sTree(right)   
            | _ -> raise (OperatorError("unknown operator"))
    sTree tree

let tree1 = Operation('+', Operation('/', Number(3.), Number(6.)), Number(1.))
let tree2 = Operation('*', Number(5.), Number(7.))
let tree4 = Operation('+', Operation('?', Number(3.), Number(6.)), Number(1.))

printfn "%A" (solveTree tree1)