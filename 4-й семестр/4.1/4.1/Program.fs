open System

let checkString (str:String) = 

    let rec check index firstEntry bkt balance sLength (str:String) =

        if index < sLength then
            let curChar = str.Chars(index)
            let balance = 
                match balance with
                | (a,b,c) -> 
                    match curChar with
                    | '(' -> (a+1,b,c)
                    | '[' -> (a,b+1,c)
                    | '{' -> (a,b,c+1)

                    | ')' -> (a-1,b,c)
                    | ']' -> (a,b-1,c)
                    | '}' -> (a,b,c-1)

                    | _ -> balance

            let bkt, firstEntry =
                match curChar with
                | ('('|'['|'{') when bkt = ' ' -> curChar, index
                | _ -> bkt, firstEntry

            match balance with
            | (a,b,c) when (a < 0 || b < 0 || c < 0) -> false             
            | (0,0,0) -> 
                if (bkt = '(' && curChar = ')') || (bkt = '[' && curChar = ']') || (bkt = '{' && curChar = '}') then
                    let str1 = str.Substring(firstEntry+1, index-firstEntry-1)
                    let str2 = str.Substring(index+1, sLength-index-1)
                    (check 0 0 ' ' (0,0,0) str1.Length str1) && (check 0 0 ' ' (0,0,0) str2.Length str2)
                else check (index+1) firstEntry bkt balance sLength str
            | _ -> check (index+1) firstEntry bkt balance sLength str

        else 
            match balance with
            | (0,0,0) when bkt = ' ' -> true
            | _ -> false
             
    check 0 0 ' ' (0,0,0) str.Length str



printfn "%A" (checkString "({()c}1 [qew( )])2q[a] ()[]")