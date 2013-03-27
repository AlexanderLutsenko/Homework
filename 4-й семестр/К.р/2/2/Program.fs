open System

let checkString (str:String) = 
    let rec check a b (str:String) =
        if (b - a) > 0 then 
            if (str.Chars(a) = str.Chars(b)) then check (a + 1) (b - 1) str
            else false
        else true
    check 0 (str.Length - 1) str

let rec ffor a b f =
    if a <= b then  
        let func = ffor (a + 1) b f      
        if f a > func then f a
        else func
    else 0

let main = 
    ffor 100 999 (
        fun a -> ffor 100 999 (fun b -> 
            if checkString (string (a * b)) then a * b
            else 0
        ) 
    )

printfn "%A" main
Console.ReadLine() |> ignore