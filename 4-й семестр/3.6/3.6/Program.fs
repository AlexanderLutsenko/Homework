let isPrime n =
    let rec check i =
        i > int (Operators.sqrt(float(n))) || (n % i <> 0 && check (i + 1))
    check 2

let primeNumbers = Seq.initInfinite (fun index ->
    let rec findPrime index current acc = 
        if acc = index then current - 1
        else
            if isPrime current then findPrime index (current + 1) (acc + 1)
            else findPrime index (current + 1) acc
    findPrime (index + 1) 2 0
    )

Seq.take 1000 primeNumbers |> Seq.iter (printf "%A ")
