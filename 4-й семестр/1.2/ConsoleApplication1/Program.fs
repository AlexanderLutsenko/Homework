open System

let fibonacci x = 
    let rec fib x acc1 acc2 = 
        if x <= 1 then acc2
        else fib (x-1) acc2 (acc1+acc2)
    fib x 0 1

printf "Введите число: "
let input = int (Console.ReadLine())
printfn "%i-е число Фибоначчи = %i" input (fibonacci input)

Console.ReadLine()
