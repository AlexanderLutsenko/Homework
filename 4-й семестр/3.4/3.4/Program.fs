type Tree =
    | Tip
    | Node of int * Tree * Tree

let heightTree tree = 
    let rec hTree tree acc =
        match tree with
        | Tip -> acc
        | Node (value, left, right) ->
            let hLeft = hTree left (acc + 1)
            let hRight = hTree right (acc + 1)
            if  hLeft > hRight then
                hLeft
            else hRight               
    hTree tree 0

let tree1 = Node (0, Node (1, Node (2, Tip, Tip), Node (3, Tip, Tip)), Node (4, Tip, Tip))
let tree2 = Node (0, Tip, Node (4, Tip, Tip))
let tree3 = Node (0, Tip, Tip)
let tree4 = Tip

printfn "%A" (heightTree tree1)