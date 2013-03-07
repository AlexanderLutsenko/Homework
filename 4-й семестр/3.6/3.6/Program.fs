let isPrime n =
    let rec check i =
        i > int(Operators.sqrt(float(n))) || (n % i <> 0 && check (i + 1))
    check 2

let primeNumerals = Seq.initInfinite (fun index ->
    let rec findPrime index current acc = 
        if acc = index then current-1
        else
            if isPrime current then 
                let acc = acc+1
                findPrime index (current+1) acc
            else findPrime index (current+1) acc
    findPrime (index+1) 2 0
    )

Seq.take 30 primeNumerals |> Seq.iter (printf "%A ")
