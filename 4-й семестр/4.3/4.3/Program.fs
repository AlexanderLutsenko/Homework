open System
open System.IO
open System.Runtime.Serialization.Formatters.Binary

let add data = 
    printf "%s" "Введите имя: "
    let name = Console.ReadLine()
    printf "%s" "Введите номер телефона: "
    let phone = Console.ReadLine()
    let data = (name, phone)::data
    printf "%s" "Данные добавлены в базу"
    data

let findByName data = 
    printf "%s" "Введите имя: "
    let name = Console.ReadLine()
    let data = List.filter (fun x -> fst x = name) data
    List.map(fun x -> printfn "%s" (snd x)) data |> ignore

let findByPhone data = 
    printf "%s" "Введите номер телефона: "
    let phone = Console.ReadLine()
    let data = List.filter (fun x -> snd x = phone) data
    List.map(fun x -> printfn "%s" (fst x)) data |> ignore

let save data source =     
    let fsOut = new FileStream(source, FileMode.Create)
    let formatter = new BinaryFormatter()
    formatter.Serialize(fsOut, box data)
    fsOut.Close()
    printf "%s" "Данные сохранены"

let load source =
    let fsIn = new FileStream(source, FileMode.Open)
    let formatter = new BinaryFormatter()
    let res = unbox (formatter.Deserialize(fsIn))
    fsIn.Close()
    printf "%s" "Данные считаны"
    res

let rec menu data source = 
    printfn "%s" "
0 - выйти
1 - добавить запись
2 - найти телефон по имени
3 - найти имя по телефону
4 - сохранить текущие данные в файл
5 - считать данные из файла"
    let command = Console.ReadKey()
    match command with 
    | c when (c.Key = ConsoleKey.D0) -> ignore
    | c ->
        let data = 
            Console.Clear()
            match c.Key with
            | ConsoleKey.D1 -> add data 
            | ConsoleKey.D2 -> findByName data
                               data
            | ConsoleKey.D3 -> findByPhone data
                               data
            | ConsoleKey.D4 -> save data source
                               data
            | ConsoleKey.D5 -> load source
            | _ -> data
        menu data source

menu [] "DataBase.dat" ignore
