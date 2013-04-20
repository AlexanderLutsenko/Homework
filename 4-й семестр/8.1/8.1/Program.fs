open System
open System.Net
open System.Threading
open System.Text.RegularExpressions

let compare (uri1 : Uri) (uri2  : Uri) = 
        uri1.AbsoluteUri.CompareTo uri2.AbsoluteUri

let removeDuplicates (uries : Uri list) =     
    let rec remDup (uries : Uri list) = 
        match uries with 
        | h1 :: h2 :: tl ->
            if compare h1 h2 <> 0 then h1 :: remDup (h2 :: tl)
            else remDup (h2 :: tl)
        | smth -> smth
    let uries = List.sortWith (fun uri1 uri2 -> compare uri1 uri2) uries
    remDup uries

let getHtml (uri : Uri) = 
    async {
        let webClient = new WebClient()
        let req = WebRequest.Create(uri)
        let! html = webClient.AsyncDownloadString(uri)
        return html
    }

let getLinks uri = 
    let html = getHtml uri |> Async.RunSynchronously 
    //let hrefRegex = new Regex("(?<=<a href=\")http://[^\"<>]+(?=\">)")
    //Включая относительные ссылки и игнорируя якори
    let hrefRegex = new Regex("(?<=<(a href|a [^<>]*? href)=\")[^#\"<>]*(?=(#.[^\"<>]*)?\"[^<>]*>)")

    let matches = hrefRegex.Matches html
    let hrefs = [for i in 0 .. matches.Count - 1 -> (matches.[i]).Value]
    let createAddress (addr : string) = 
        if addr.StartsWith "http://" then addr 
        else 
            let addr = 
                if addr.StartsWith "/" then addr.Substring 1
                else addr
            uri.AbsoluteUri + addr
    let uries = List.map (fun (addr : string) -> Uri (createAddress addr)) hrefs    
    let uries = List.filter (fun (link : Uri) -> if compare link uri <> 0 then true else false) (removeDuplicates uries)
    printfn "Обнаружено ссылок - %i, из них уникальных - %i" hrefs.Length uries.Length    
    uries
    
let countCharacters uri lockObj = 
    async {
        let! html = getHtml(uri)
        let address = uri.AbsoluteUri
        let charNumber = html.Length
        Monitor.Enter lockObj
        printfn "%s --- %i" address charNumber
        Monitor.Exit lockObj
    }  

let main = 
    printf "Введите адрес страницы: "
    let address = 
        let addr = Console.ReadLine()
        if not (addr.StartsWith "http://") then
            "http://" + addr
        else addr
    let lockObj = new Object()
    let links = getLinks(Uri (address, UriKind.RelativeOrAbsolute))
    let tasks = List.map (fun link -> countCharacters link lockObj) links
    tasks |> Async.Parallel |> Async.RunSynchronously |> ignore
    Console.Read() |> ignore

main