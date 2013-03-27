open System

exception EmptyQueueException

type Queue<'a>() =
    let mutable list : 'a list = []
    member this.Enqueue elem = 
        list <- elem :: list
    member this.Dequeue = 
        match list with
        | hd :: tl -> 
            list <- tl
            hd
        | [] -> raise EmptyQueueException

let qu = Queue<string>()
qu.Enqueue "sdfdf"
printfn "%A" qu.Dequeue