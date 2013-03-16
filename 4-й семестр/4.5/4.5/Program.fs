type BinTree = 
    | Tip
    | Node of int * BinTree * BinTree

let rec treeMap func tree =
    match tree with
    | Tip -> Tip
    | Node (value, left, right) -> Node (func value, treeMap func left, treeMap func right)

printfn "%A" ( treeMap (fun x -> 2 * x + 1) (Node (1, Node (2, Tip, Tip), Node (3, Tip, Tip))) )