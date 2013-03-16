type Tree =
    | Number of float
    | Add of Tree * Tree
    | Ded of Tree * Tree
    | Mul of Tree * Tree    
    | Div of Tree * Tree

let solveTree tree = 
    let rec sTree tree =
        match tree with
        | Number(value) -> value 
        | Add (left, right) -> sTree(left) + sTree(right)
        | Ded (left, right) -> sTree(left) - sTree(right)
        | Mul (left, right) -> sTree(left) * sTree(right)
        | Div (left, right) -> sTree(left) / sTree(right)
    sTree tree

let tree = Add (Div (Number 3., Number 6.), Number 1.)
printfn "%A" (solveTree tree)
