open System
open System.IO
open System.Collections.Generic

type Computer(os, securityRank, id) = 
    let mutable id = id
    let mutable securityRank = securityRank
    let mutable os = os
    let mutable connections : Computer list = []   
    let mutable infected = false

    member this.isInfected 
        with set(b) = infected <- b
        and get() = infected
    member this.getOS = os
    member this.getID = id

    member this.SetConnections (connects, (computers : Computer list)) = 
        connections <- List.fold2(fun acc isConnectedWith comp -> if isConnectedWith then comp :: acc else acc) [] connects computers
    
    member this.Attack() = 
         if infected then
            let attk (comp : Computer) = 
                if ((not comp.isInfected) && comp.TryToInfect()) then   
                    printfn "%s" ("    Компьютер " + this.getID.ToString() + " заразил компьютер " + comp.getID.ToString() + "(" + comp.getOS + ")") 
                else ()
            List.iter (fun comp -> attk comp) connections  

    member this.TryToInfect() = 
        if (System.Random().NextDouble()) < securityRank then 
            this.isInfected <- true
            true
        else false
        


type ComputerNetwork (compCount, adjMatrix, operatingSystems : string list, securityRanks : Dictionary<string, float>) = 
    let mutable compCount = compCount
    let mutable computers : Computer list = []
    let mutable step = 0
    let mutable dead = false

    do
        computers <- List.mapi (fun id os -> new Computer(os, (securityRanks.Item os), id)) operatingSystems
        List.iter2 (fun (comp : Computer) connects -> comp.SetConnections(connects, computers)) computers adjMatrix
        printfn "%s" "Компьютерная сеть запущена"

    member this.isDead = dead
    
    member this.InfectComp(infComp) = 
        (computers.Item infComp).isInfected <- true
        printfn "%s" ("Компьютер " + infComp.ToString() + " подхватил вирус из интернета!")

    member this.NextStep() = 
        step <- step + 1
        let infectedComps = List.fold (fun acc (comp : Computer) -> if comp.isInfected then comp :: acc else acc) [] computers
        if (not dead) && (infectedComps.Length < compCount) then
            printfn "%s" ("Шаг " + step.ToString() + ":")
            List.iter (fun (comp : Computer) -> comp.Attack()) infectedComps
        else 
            printfn "%s" ("Все компьютеры заражены!")
            dead <- true



type main() = 
    static member start() =         
        let net : ComputerNetwork = main.createNetwork (Directory.GetCurrentDirectory() + "\input.txt")
        net.InfectComp(0)
        printfn "%s" "<AnyKey> - следующий шаг"
        while not net.isDead do
            Console.ReadKey()|>ignore
            net.NextStep()
        Console.ReadKey() |> ignore

    static member createNetwork file =        
        try
            let reader = File.OpenText(file)

            let toList format (input : string) = 
                List.map (fun obj -> format obj) 
                         (Array.toList (input.ToLower().Split([|' '|], StringSplitOptions.RemoveEmptyEntries)))

            let operatingSystems = toList string (reader.ReadLine())
            let compCount = operatingSystems.Length
            let adjMatrix = 
                let rec adj index = 
                    let input = List.map (fun x -> match x with | 1 -> true | 0 -> false | _ -> raise (new Exception()) ) 
                                         (toList int (reader.ReadLine()))
                    if index = compCount then [input]
                    else input :: adj (index+1)                                        
                adj 1

            List.iter (fun (l : bool list) -> if l.Length = compCount then () else raise (new Exception()) ) adjMatrix

            let securityRanks = new Dictionary<string, float>()
            securityRanks.Add("windows", 0.7)
            securityRanks.Add("macos", 0.3)
            securityRanks.Add("linux", 0.15)

            reader.Close()

            new ComputerNetwork (compCount, adjMatrix, operatingSystems, securityRanks)
        with 
            | :? IO.IOException as exc -> raise exc
            | _ -> raise (new IO.InvalidDataException("Файл имеет неверный формат ввода"))
                         
main.start()