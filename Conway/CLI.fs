namespace Conway

open System
open System.Numerics
open System.Text.RegularExpressions

module CLI =

    let (|MatchRegex|_|) p s =
      let r = Regex(p).Match(s)
      if r.Success then
        Some (List.tail [ for x in r.Groups -> x.Value ])
      else
        None

    let (|Int|_|) s =
      match System.Int32.TryParse(s) with
        | (true, x) -> Some(x)
        | _ -> None

    let (|Boolean|_|) s =
      match System.Boolean.TryParse(s) with
        | (true, x) -> Some(x)
        | _ -> None

    let (|BigInt|_|) s =
      match BigInteger.TryParse(s) with
        | (true, x) -> Some(x)
        | _ -> None

    let (|SetCommand|_|) s =
      let p = "^\s*set\s+([0-9]+)\s+([0-9]+)\s+(true|false)\s*$"
      match s with
        | MatchRegex p [ BigInt x; BigInt y; Boolean v ] -> Some(x, y, v)
        | _ -> None

    let (|GetCommand|_|) s =
      let p = "^\s*get\s+([0-9]+)\s+([0-9]+)\s*$"
      match s with
        | MatchRegex p [ BigInt x; BigInt y ] -> Some(x, y)
        | _ -> None

    let (|NextCommand|_|) s =
      let p = "^\s*next\s*$"
      let m = Regex(p).Match(s)
      if m.Success && m.Groups.Count = 1 then
        Some ()
      else
        None

    let (|RunCommand|_|) s =
      let p = "^\s*run\s+([0-9]+)\s*$"
      match s with
        | MatchRegex p [ Int n ] -> Some(n)
        | _ -> None

    let (|ShowCommand|_|) s =
      let p = "^\s*show\s*$"
      let m = Regex(p).Match(s)
      if m.Success && m.Groups.Count = 1 then
        Some ()
      else
        None

    let rec commandLoop state =
      Console.Write "> "
      let rawCommand = Console.ReadLine().ToLower()
      match rawCommand with
        | NextCommand ->
          state |> CellularAutomata.next |> commandLoop
        | SetCommand (x, y, v) ->
          state |> CellularAutomata.set x y v |> commandLoop
        | GetCommand (x, y) ->
          state |> CellularAutomata.get x y |> Console.WriteLine
          commandLoop state
        | RunCommand n ->
          seq { 0 .. n } |>
            Seq.fold (fun s _ -> CellularAutomata.next s) state |>
            commandLoop
        | ShowCommand ->
          state |> CellularAutomata.show |> Console.WriteLine
        | _ ->
          Console.WriteLine "Unrecognized command. "
          Console.WriteLine ()
          Console.WriteLine "Why not try one of these? "
          Console.WriteLine ()
          Console.WriteLine "  set [0-9]+ [0-9]+ (true|false)"
          Console.WriteLine "  get [0-9]+ [0-9]+"
          Console.WriteLine "  next"
          Console.WriteLine "  run [0-9]+"
          Console.WriteLine "  show"
          Console.WriteLine ()
          Console.WriteLine "Use ctrl + c to exit"
      commandLoop state
