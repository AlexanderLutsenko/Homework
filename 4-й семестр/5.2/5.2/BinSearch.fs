open System
open System.Collections

exception EnumeratorException of string

type Element (value : IComparable) =
    let mutable m_value = value
    let mutable m_left = None
    let mutable m_right = None
    member this.Value = m_value
    member this.Left
        with get() = m_left
        and set elem = m_left <- elem
    member this.Right
        with get() = m_right
        and set elem = m_right <- elem

type TreeEnumerator (tree : Element option) as this = 
    let mutable m_stack : Element option list = []
    let mutable m_current = null
    do (this :> IEnumerator).Reset()
    interface IEnumerator with  
        member this.Current = 
            match m_current with
            | null -> raise (EnumeratorException "Element pointer out of range")
            | _ -> m_current :> obj     
        member this.MoveNext() = 
            match m_stack with                                                       
            | hd :: tl -> 
                let cur = hd.Value
                m_stack <- tl              
                m_stack <-
                    if cur.Right <> None then cur.Right :: m_stack
                    else m_stack
                m_stack <-
                    if cur.Left <> None then cur.Left :: m_stack
                    else m_stack
                m_current <- cur.Value
                true
            | [] -> 
                m_current <- null
                false
        member this.Reset() = 
            m_stack <- 
                m_current <- null
                match tree with
                | None -> []
                | _ -> [tree]     

type BinSearchTree<'c when 'c :> IComparable> (values : 'c list) as this =
    let mutable m_root = None
    do List.iter (fun value -> this.AddElement value) values  
             
    member this.AddElement (value : 'c) =
        match m_root with
        | None -> m_root <- Some(Element value)
        | Some(_) -> 
            let rec add (node : Element option) (value : 'c) = 
                match value.CompareTo (node.Value.Value)  with
                | x when x < 0 -> 
                    if node.Value.Left = None then node.Value.Left <- Some(Element value) 
                    else add node.Value.Left value
                | _ -> 
                    if node.Value.Right = None then node.Value.Right <- Some(Element value)
                    else add node.Value.Right value
            add m_root value

    member this.IsExists (value : 'c) = 
        let rec isEx (node : Element option) value = 
            match node with
            | None -> false
            | Some(elem) when value = elem.Value  -> true
            | Some(elem) when value < elem.Value -> isEx elem.Left value
            | Some(elem) -> isEx elem.Right value
        isEx m_root value

    override this.ToString() =
        let rec LSF (node : Element option) =
            match node with
            | None -> "*"
            | Some(elem) -> 
                if elem.Left <> None || elem.Right <> None then
                    string elem.Value + "(" + LSF elem.Left + "," + LSF elem.Right + ")"
                else string elem.Value
        LSF m_root

    interface IEnumerable with
        member this.GetEnumerator() = new TreeEnumerator(m_root) :> IEnumerator

    new () = BinSearchTree []


let main = 
    printfn "%s" "Введите элементы дерева: "
    let input = (Console.ReadLine()).Split([|' '|])
    let inputList = List.map (fun c -> double c) (Array.toList input)
    let tree = BinSearchTree inputList
    printfn "%A" (tree.ToString())
    printfn "%A" tree
    let enum = (tree :> IEnumerable).GetEnumerator()

    for elem in tree do
        printfn "%A" elem

    Console.ReadLine() |> ignore
    
main