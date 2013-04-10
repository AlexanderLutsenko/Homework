open System.ComponentModel

let mutable result = 0
let mutable completed = [|for i in 0..99 -> false|]
let mutable array = [|for i in 0..999999 -> 1|]
let mutable compl = false

for i in 0 .. 99 do      
    let worker = new BackgroundWorker()
    worker.DoWork.Add(fun args -> for j in 0..9999 do result <- result + array.[i * 10000 + j])
    worker.RunWorkerCompleted.Add(fun args -> completed.[i] <- true)
    worker.RunWorkerAsync()

while not compl do
    compl <- Array.forall (fun b -> b = true) completed
    
printfn "Странный результат: %A" result
System.Console.ReadLine() |> ignore