open System

let multiplication x = 
    let rec mul x acc =
        if x = 0 then acc
        else mul (x/10) ((x%10)*acc)     
    if x = 0 then 0
    else mul x 1

printf "Введите число: "
let input = int (Console.ReadLine())
printfn "Пр-е цифр числа %i = %i" input (multiplication input)

Console.ReadLine()
