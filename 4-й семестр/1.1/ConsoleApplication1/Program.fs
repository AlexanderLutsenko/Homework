open System

let factorial x = 
    let rec fact x acc = 
        if x < 0 then None
        else if x = 0 then Some acc
        else fact (x - 1) (acc * x)
    fact x 1

printf "Введите число: "
let input = int (Console.ReadLine())
printfn "%A! = %A" input (factorial input)

Console.ReadLine() |> ignore
