open System

let factorial x = 
    let rec fact x acc = 
        if x <= 1 then acc
        else fact (x-1) (acc*x)
    fact x 1

printf "Введите число: "
let input = int (Console.ReadLine())
printfn "%i! = %i" input (factorial input)

Console.ReadLine()
