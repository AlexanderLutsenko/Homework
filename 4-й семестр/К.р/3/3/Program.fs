open System

exception EmptyQueueException

type Queue<'a>() =
    let mutable list : 'a list = []
    member this.Enqueue elem = 
        list <- list @ [elem]
    member this.Dequeue = 
        match list with
        | hd :: tl -> 
            list <- tl
            hd
        | [] -> raise EmptyQueueException
    member this.Count = List.length list

let qu = Queue<string>()
for i = 1 to 5 do
    qu.Enqueue (string i)
printfn "%A" qu.Count
for i = 1 to qu.Count do
    printfn "%A" qu.Dequeue
printfn "%A" qu.Count