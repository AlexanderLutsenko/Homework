open System
exception OperatorError of string

type Tree =
    | Variable
    | Number of float
    | Operation of char * Tree * Tree


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
            | b when (b >= 0) -> check str len (index+1) balance
            | _ -> false
        | _ -> 
            match balance with
            | 0 -> true
            | _ -> false    
    check str str.Length 0 0    

let rec deleteBrackets (str:String) = 
    let len = str.Length
    let first = str.Chars(0)
    let last = str.Chars(len-1) 
    match str with
    | _ when first = ' ' -> deleteBrackets (str.Substring(1))
    | _ when last = ' ' -> deleteBrackets (str.Substring(0, len-1))
    | _ when (first = '(' && last = ')') -> 
        let subStr = str.Substring(1, len-2)
        match checkBktSeq subStr with
        | true -> deleteBrackets (subStr)
        | false -> str
    | _ -> str

let findOperator (str:String) = 
    let rec find (str:String) len balance index rememberMul = 
        if index < len then
            let current = str.Chars(index)
            let curBal = checkCurrent (current)
            let balance = balance + curBal           
            if balance = 0 then
                match current with
                | ('+'|'-') -> index
                | ('*'|'/') -> 
                    let rememberMul = if rememberMul = -1 then index else rememberMul
                    find str len balance (index+1) rememberMul
                | _ -> find str len balance (index+1) rememberMul
            else find str len balance (index+1) rememberMul
        else rememberMul
    find str str.Length 0 0 -1

let rec strToTree (str:String) =
    let str = deleteBrackets str
    let operNum = findOperator str
    if operNum >= 0 then
        let str1 = str.Substring(0, operNum)
        let str2 = str.Substring(operNum+1)
        Operation ( str.Chars(operNum), strToTree (str1), strToTree (str2) )
    else
        match str with
        | "x" -> Variable
        | _ -> Number (float str)

let rec treeToLSF tree = 
    match tree with
    | Variable -> "x"
    | Number(value) -> string value
    | Operation (oper,left,right) -> "(" + treeToLSF(left) + string oper + treeToLSF(right) + ")"


let rec reduce tree = 
        let tree = 
            match tree with
            | Operation (oper,left,right) -> Operation (oper, reduce left, reduce right)
            | tree -> tree
        let tree = 
            match tree with
            | Operation ('*', Number value, any) -> Operation('*', any, Number value)
            | Operation ('+', Number value, any) -> Operation('+', any, Number value)
            | _ -> tree       
        let tree = 
            match tree with
            | Operation (('*'|'/'), left, Number 1.) -> left        
            | Operation (('+'|'-'), left, Number 0.) -> left
            | Operation ('*', left, Number 0.) -> Number 0.
            | Operation ('/', Number 0., right) -> Number 0.

            | Operation ('-', left, right) when left = right -> Number 0.
            | Operation ('/', left, right) when left = right -> Number 1.

            | Operation (oper, Number val1, Number val2) -> 
                match oper with
                | '+' -> Number (val1+val2)
                | '-' -> Number (val1-val2)
                | '*' -> Number (val1*val2)
                | '/' -> Number (val1/val2)
                | _ -> Operation (oper, Number val1, Number val2)
            | _ -> tree 
        tree


let derivateTree tree =    
    let rec dTree tree =
        match tree with
        | Number(value) -> Number 0.
        | Variable -> reduce (Number 1.)
        | Operation(operator, left, right) ->
            match operator with           
            | '+' -> Operation ('+',dTree left,dTree right)
            | '-' -> Operation ('-',dTree left,dTree right)
            | '*' -> Operation (  '+',  Operation('*',dTree left,right),  Operation('*',left,dTree right)  ) 
            | '/' -> Operation (
                                '/',
                                Operation (  '-',  Operation('*',dTree left,right),  Operation('*',left,dTree right)  ),
                                Operation ('*',right,right) 
                                )                        
            | _ -> raise (OperatorError("unknown operator"))
    reduce (dTree tree)


let str = "x/6+x*x "
let tree = strToTree str
let tr = derivateTree tree

printfn "%A" (str)
printfn "%A" (treeToLSF tr)
