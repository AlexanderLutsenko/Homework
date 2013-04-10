open System
open System.Collections.Generic
open System.Collections

exception EnumeratorException of string

type Element<'c> (value : 'c) =
    let mutable m_value = value
    let mutable m_left : Element<'c> option = None
    let mutable m_right : Element<'c> option = None
    member this.Value = m_value
    member this.Left
        with get() = m_left
        and set elem = m_left <- elem
    member this.Right
        with get() = m_right
        and set elem = m_right <- elem

type TreeEnumerator<'c> (tree : Element<'c> option) as this = 
    let mutable m_stack : Element<'c> option list = []
    let mutable m_current = None
    do (this :> IEnumerator<'c>).Reset()
    interface IEnumerator<'c> with  
        member this.Current = 
            match m_current with
            | None -> raise (EnumeratorException "Element pointer out of range")
            | Some(value) -> value
        member this.Current = box (this :> IEnumerator<'c>).Current        
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
                m_current <- Some cur.Value
                true
            | [] -> 
                m_current <- None
                false
        member this.Reset() = 
            m_stack <- 
                m_current <- None
                match tree with
                | None -> []
                | _ -> [tree] 
        member this.Dispose() = ()    

type BinSearchTree<'c when 'c : comparison> (values : 'c list) as this =
    let mutable m_root = None
    do List.iter (fun value -> this.AddElement value) values  
             
    member this.AddElement value =
        match m_root with
        | None -> m_root <- Some(Element value)
        | Some(_) -> 
            let rec add (node : Element<'c> option) value = 
                if value < node.Value.Value then
                    if node.Value.Left = None then node.Value.Left <- Some(Element value) 
                    else add node.Value.Left value
                else
                    if node.Value.Right = None then node.Value.Right <- Some(Element value)
                    else add node.Value.Right value
            add m_root value

    member this.IsExists value = 
        let rec isEx (node : Element<'c> option) value = 
            match node with
            | None -> false
            | Some(elem) when value = elem.Value  -> true
            | Some(elem) when value < elem.Value -> isEx elem.Left value
            | Some(elem) -> isEx elem.Right value
        isEx m_root value 

    override this.ToString() =
        let rec LSF (node : Element<'c> option) =
            match node with
            | None -> "*"
            | Some(elem) -> 
                if elem.Left <> None || elem.Right <> None then
                    elem.Value.ToString() + "(" + LSF elem.Left + "," + LSF elem.Right + ")"
                else elem.Value.ToString()
        LSF m_root 

    interface IEnumerable<'c> with
        member this.GetEnumerator() = new TreeEnumerator<'c>(m_root) :> IEnumerator<'c>
        member this.GetEnumerator() = (this :> IEnumerable<'c>).GetEnumerator() :> IEnumerator

    new () = BinSearchTree []


let main = 
    printfn "%s" "Введите элементы дерева: "
    let input = (Console.ReadLine()).Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    let inputList = List.map (fun c -> double c) (Array.toList input)
    let tree = BinSearchTree<double> inputList
    printfn "%s" (tree.ToString())
    printfn "%A" tree

    let enum = (tree :> IEnumerable<_>).GetEnumerator()

    for elem in tree do
        printfn "%A" elem
    
    Console.ReadLine() |> ignore
    
main 