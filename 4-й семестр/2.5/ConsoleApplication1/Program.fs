open System

let checkString (str:String) = 
    let rec check a b (str:String) =
        if (b - a) > 1 then 
            if (str.Chars(a) = str.Chars(b)) then check (a+1) (b-1) str
            else false
        else true
    check 0 (str.Length-1) str

printf "Введите строку: "
let input = Console.ReadLine()
printfn "%b" (checkString input)

Console.ReadLine()
