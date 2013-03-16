open System

let checkString (str:String) = 
    let rec check (str:String) sLength index (stack : char list option) =
        if index < sLength then
            let chr = str.Chars(index)
            let stack = 
                match chr with
                | ('('| '[' | '{') -> Some (chr :: stack.Value)
                | _ -> stack
            let stack = 
                match stack with
                | Some (hd :: tl) ->
                    match chr with
                    | ')' when hd = '(' -> Some tl
                    | '}' when hd = '{' -> Some tl
                    | ']' when hd = '[' -> Some tl
                    | (')' | ']' | '}') -> None
                    | _ -> stack
                 | _ ->
                    match chr with
                    | (')' | ']' | '}') -> None
                    | _ -> stack
            if stack = None then false
            else check str sLength (index+1) stack               
        else 
            match stack with
            | Some [] -> true
            | _ -> false             
    check str str.Length 0 (Some [])

printfn "%A" (checkString "({()c}1 [qew( )])2q[a] ()[]")