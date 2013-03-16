let isPrime n =
    let rec check i =
        i > int (Operators.sqrt(float(n))) || (n % i <> 0 && check (i + 1))
    check 2

let primeNumbers1 = Seq.initInfinite (fun index ->
    let rec findPrime index current acc = 
        if acc = index then current - 1
        else
            if isPrime current then findPrime index (current + 1) (acc + 1)
            else findPrime index (current + 1) acc
    findPrime (index + 1) 2 0
    )

let primeNumbers2 = Seq.unfold (fun n -> let rec findPrime n =
                                            if isPrime n then Some (n, n+1)
                                            else findPrime (n+1)
                                         findPrime n 
                                         ) 2

Seq.take 100000 primeNumbers2 |> Seq.iter (printf "%A ")
            
