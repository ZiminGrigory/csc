namespace FSharpHW

module HW =

    (* task 1 *)
    let fib n =
        let rec fib' acc acc' = function
            | 0 -> acc
            | n -> fib' acc' (acc + acc') (n - 1)
        fib' 0I 1I n

    (* task 2 *)
    let reverse xs =
        let rec reverse' acc = function
            | [] -> acc
            | x::xs' -> reverse' (x::acc) xs'
        reverse' [] xs

    (* task 3 *)
    let private merge xs ys =
        let rec revAppend xs = function
            | [] -> xs
            | y::ys -> revAppend (y::xs) ys

        let rec merge' acc xs ys =
            match xs, ys with
            | [], xs
            | xs, [] -> revAppend acc xs
            | x::xs', y::ys' ->
                if x < y
                then merge' (x::acc) xs' ys
                else merge' (y::acc) xs ys'

        merge' [] xs ys |> reverse

    let mergeSort xs =
        let rec mergeSort' xs acc =
            match xs, acc with
            | [], [] -> []
            | [], [x] -> x
            | [], _ -> mergeSort' acc []
            | [x], [] -> x
            | [x], a::acc' -> mergeSort' (merge x a :: acc') []
            | x::y::xs, _ -> mergeSort' xs (merge x y :: acc)

        List.map List.singleton xs |> mergeSort' []


    (* task 4 *)
    type ArythmeticsTree =
    | Value of double
    | Add of ArythmeticsTree * ArythmeticsTree
    | Sub of ArythmeticsTree * ArythmeticsTree
    | Mul of ArythmeticsTree * ArythmeticsTree
    | Div of ArythmeticsTree * ArythmeticsTree

    let rec evaluate = function
        | Value x -> x
        | Add (a, b) -> evaluate a + evaluate b
        | Sub (a, b) -> evaluate a - evaluate b
        | Mul (a, b) -> evaluate a * evaluate b
        | Div (a, b) -> evaluate a / evaluate b


    (* task 5 *)
    let private isPrime (number : bigint) =
        seq {bigint(2) .. bigint(sqrt(double number))}
        |> Seq.exists (fun x -> number % x = bigint(0)) |> not

    let primeNumbers =
        Seq.initInfinite (fun i -> i + 2)
        |> Seq.map (fun i -> bigint(i))
        |> Seq.filter isPrime