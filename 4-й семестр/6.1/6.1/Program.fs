type RoundedBuilder (accuracy : int) = 
    let a = 10. ** (float accuracy)
    member this.Bind ((x : float), (rest : float -> float)) =        
        let round x = System.Math.Round (x * a) / a
        round (rest x)
    member this.Return x = x

let rounding x = RoundedBuilder x
let expression =
    rounding 3 {
        let! a = 2. / 12.
        let! b = 3.5
        return a / b
    }
    
printfn "%A" expression