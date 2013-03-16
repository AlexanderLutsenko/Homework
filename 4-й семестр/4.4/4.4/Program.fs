open System
exception OperatorError of string

type Tree =
    | Variable
    | Number of float
    | Add of Tree * Tree
    | Ded of Tree * Tree
    | Mul of Tree * Tree    
    | Div of Tree * Tree



let checkCurrent chr = 
    match chr with
    | '(' ->  1
    | ')' ->  -1
    | _ ->  0

let checkBktSeq (str:String) = 
    let rec check (str:String) len index balance = 
        match index with
        | i when i < len ->
            let curBal = checkCurrent (str.Chars(index))
            let balance = balance + curBal
            match balance with
            | b when (b >= 0) -> check str len (index + 1) balance
            | _ -> false
        | _ -> 
            match balance with
            | 0 -> true
            | _ -> false    
    check str str.Length 0 0    

let rec deleteBrackets (str:String) = 
    let len = str.Length
    let first = str.Chars 0
    let last = str.Chars (len - 1) 
    match str with
    | _ when first = ' ' -> deleteBrackets (str.Substring 1)
    | _ when last = ' ' -> deleteBrackets (str.Substring(0, len - 1))
    | _ when (first = '(' && last = ')') -> 
        let subStr = str.Substring(1, len - 2)
        match checkBktSeq subStr with
        | true -> deleteBrackets subStr
        | false -> str
    | _ -> str

let findOperator (str:String) = 
    let rec find (str:String) len balance index rememberMul = 
        if index < len then
            let current = str.Chars index
            let curBal = checkCurrent current
            let balance = balance + curBal           
            if balance = 0 then
                match current with
                | ('+'|'-') -> index
                | ('*'|'/') -> 
                    let rememberMul = if rememberMul = -1 then index else rememberMul
                    find str len balance (index + 1) rememberMul
                | _ -> find str len balance (index + 1) rememberMul
            else find str len balance (index + 1) rememberMul
        else rememberMul
    find str str.Length 0 0 -1

let rec strToTree (str:String) =
    let str = deleteBrackets str
    let operNum = findOperator str
    if operNum >= 0 then
        let str1 = str.Substring(0, operNum)
        let str2 = str.Substring(operNum + 1)
        match str.Chars operNum with
        | '+' -> Add (strToTree str1, strToTree str2)
        | '-' -> Ded (strToTree str1, strToTree str2)
        | '*' -> Mul (strToTree str1, strToTree str2)
        | '/' -> Div (strToTree str1, strToTree str2)
        | _ -> raise (OperatorError("WTF?!"))
    else
        match str with
        | "x" -> Variable
        | _ -> Number (float str)

let rec treeToLSF tree = 
    match tree with
    | Variable -> "x"
    | Number(value) -> string value
    | Add (left, right) -> "(" + treeToLSF(left) + "+" + treeToLSF(right) + ")"
    | Ded (left, right) -> "(" + treeToLSF(left) + "-" + treeToLSF(right) + ")"
    | Mul (left, right) -> treeToLSF(left) + "*" + treeToLSF(right)
    | Div (left, right) -> treeToLSF(left) + "/" + treeToLSF(right)



let rec reduce tree = 
        let tree = 
            match tree with
            | Add (left, right) -> Add (reduce left, reduce right)
            | Ded (left, right) -> Ded (reduce left, reduce right)
            | Mul (left, right) -> Mul (reduce left, reduce right)
            | Div (left, right) -> Div (reduce left, reduce right)
            | _ -> tree
        let tree = 
            match tree with
            | Mul (Number value, any) -> Mul (any, Number value)
            | Add (Number value, any) -> Add (any, Number value)
            | _ -> tree       
        let tree = 
            match tree with
            | Mul (left, Number 1.) | Div (left, Number 1.) -> left        
            | Add (left, Number 0.) | Ded (left, Number 0.) -> left
            | Mul (left, Number 0.) -> Number 0.
            | Div (Number 0., right) -> Number 0.

            | Ded (left, right) when left = right -> Number 0.
            | Div (left, right) when left = right -> Number 1.

            | Add (Number val1, Number val2) -> Number (val1 + val2)
            | Ded (Number val1, Number val2) -> Number (val1 - val2)
            | Mul (Number val1, Number val2) -> Number (val1 * val2)
            | Div (Number val1, Number val2) -> Number (val1 / val2)
            | _ -> tree 
        tree

let derivateTree tree =    
    let rec dTree tree =
        match tree with
        | Number(value) -> Number 0.
        | Variable -> reduce (Number 1.)
        | Add (left, right) -> Add (dTree left,dTree right)
        | Ded (left, right) -> Ded (dTree left,dTree right) 
        | Mul (left, right) -> Add ( Mul (dTree left,right), Mul (left,dTree right) ) 
        | Div (left, right) -> Div (
                                    Ded ( Mul (dTree left, right),  Mul (left, dTree right) ),
                                    Mul (right, right) 
                                    )                        
    reduce (dTree tree)

let str = "x/6+x*x "
let tree = strToTree str
let tr = derivateTree tree

printfn "%s" (str)
printfn "%s" (treeToLSF tr)
