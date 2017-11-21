open System
open System.Threading
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Conway
open Conway.CLI

// A simple serialization function for our state. 
// For a more complex app, a library would be appropriate. 
let serializeBoard (board : CellularAutomata.Board) = 
  let serializePosition (x, y) = 
    "{ x: " + string x + ", y: " + string y + "}"
  let serializeAlive x = 
    "[ " + 
    (
      x |> 
      Set.toSeq |> 
      Seq.map serializePosition |> 
      String.concat ", "
    ) + 
    " ]"
  "{ width: " + 
  string board.width + ", height: " + 
  string board.height + ", alive: " + 
  (serializeAlive board.alive) + " }"

[<EntryPoint>]
let main argv = 

  let cts = new CancellationTokenSource()
  let conf = { defaultConfig with cancellationToken = cts.Token }

  // To keep things simple, we have a single mutable variable as the state. 
  // CEs would be a good choice for more complex code, and would allow
  // greater code sharing between the CLI and server, but this 
  // approach is definitely the easiest to understand! 
  let mutable state = Map.empty 

  let handleShow (name : string) = request (fun _ ->
    match state |> Map.tryFind name with
    | Some x -> x |> serializeBoard |> OK
    | None -> RequestErrors.NOT_FOUND "Automata not found")

  let handleSet (name : string) = request (fun r -> 
    match state |> Map.tryFind name with
    | Some b -> 
      let x = 
        match r.queryParam "x" with
        | Choice1Of2 (BigInt x) -> x
        | _ -> 0I
      let y = 
        match r.queryParam "y" with
        | Choice1Of2 (BigInt x) -> x
        | _ -> 0I     
      let v = 
        match r.queryParam "v" with
        | Choice1Of2 (Boolean x) -> x
        | _ -> true
      state <- state |> Map.add name (b |> CellularAutomata.set x y v)
      OK <| "set (" + string x + ", " + string y + ") to " + string v
    | None -> RequestErrors.NOT_FOUND "Automata not found")

  let handleCreate (name : string) = request (fun r -> 
    let width = 
      match r.queryParam "width" with
      | Choice1Of2 (BigInt x) -> x
      | _ -> 64I
    let height = 
      match r.queryParam "height" with
      | Choice1Of2 (BigInt x) -> x
      | _ -> 64I
    state <- state |> Map.add name (CellularAutomata.empty width height)
    OK "Done")

  let handleNext (name : string) = request (fun _ -> 
    match state |> Map.tryFind name with
    | Some b -> 
      state <- state |> Map.add name (b |> CellularAutomata.next)
      OK "Done next"
    | None -> RequestErrors.NOT_FOUND "Automata not found")

  let app = 
    choose
        [ 
          GET >=> choose
            [ 
              pathScan "/%s" handleShow
              path "/goodbye" >=> OK "Good bye GET"
            ]
          PUT >=> choose 
            [ 
              pathScan "/%s/cell" handleSet
              pathScan "/%s" handleCreate 
            ]
          POST >=> choose
            [ 
              pathScan "/%s/next" handleNext
            ]
        ]


  let _, server = startWebServerAsync conf app
    
  Async.Start(server, cts.Token)
  printfn "Make requests now"
  Console.ReadKey true |> ignore
    
  cts.Cancel()

  0
