let isPrime n =
    let rec check i =
        i > int (Operators.sqrt(float(n))) || (n % i <> 0 && check (i + 1))
    check 2

let rec findPrime n =
    if isPrime n then Some (n, n + 1)
    else findPrime (n + 1)

let primeNumbers = Seq.unfold (fun n -> findPrime n) 2

Seq.take 100500 primeNumbers |> Seq.iter (printf "%A ")
