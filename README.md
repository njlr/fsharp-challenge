# RealSmart F# Challenge

Welcome to the RealSmart F# Coding Challenge. Please submit your solution to a public GitHub repository and email the link to leemcsorley@gmail.com with the subject title "F# Challenge".

## Section 1 - Sparse Functional Cellular Automata

The goal is to create an implementation of a cellular automata that follows the standard Conway's Game of Life rules.
* https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life. 

The implementation should be designed to enable the calculation and storage of a potentially huge grid where the dimensions are specified by a BigInteger, but where the number of active cells at any given moment is relatively small. It should also be a purely functional implementation using only persistent functional data structures.

You should implement at the very least these functions in a module called `CellularAutomata`:

```fsharp
// creates a new cellular automata with all 'dead' cells for the given x and y dimensions
val empty: x:bigint -> y:bigint -> CellularAutomata

// returns true if the cell at the specified coordinates is 'live'
val get: x:bigint -> y:bigint -> CellularAutomata -> bool

// returns a cellular automata with the cell set to the specified state at the specified coordinates
val set: x:bigint -> y:bigint -> bool -> CellularAutomata -> CellularAutomata

// returns a cellular automata that is advanced to the next state
val next: CellularAutomata -> CellularAutomata
```

## Section 2 - Command line Interface

Create a simple command-line based interface for your automata. The interface should expose and parse the `get`, `set` and `next` functions of the module above and additionally a `run` function that will take an integer and advance the cellular automata that number of steps. Each command-line session will begin with an empty cellular automata and will be used implicitly throughout when interpreting all the commands.

(Bonus points for a demonstration of the use of F# Active Patterns for parsing the commands)

#### a) Easy option
Implement it any way you like.

#### b) Harder option
Implement it using a monadic approach with F#'s Computation Expressions. The computation expression should make implicit the 'state' of the current cellular automata and also abstract away the input/output functions into the monad (like an IO monad) so that it is a 'purely functional style'. 

Below is an example of the 'style' of the code. (This is not how the interface should work, just an example of the type of code the implementation of the computation expression should allow)

```fsharp
let ca = new CABuilder()

let example =
    ca {
        do! set 3 2 true
        do! set 3 3 true
        do! set 3 4 true
        do! putStr "advancing one step"
        do! next
        do! putLine "enter number of iterations"
        let! n = getLine |> int
        for i in 1..n do
            do! next
        do! putLine "enter x coordinate"
        let! x = getLine |> bigint.Parse
        do! putLine "enter y coordinate"
        let! y = getLine |> bigint.Parse
        let! cellState = get x y
        if cellState then
            do! putLine "cell is alive"
        else
            do! putLine "cell is dead"
    } |> CAState.run (CellularAutomata.empty)
```

## Section 3 - Multiple Cellular Automata Server API

Package your cellular automata service into a REST API using Suave. 

* https://suave.io/

The API should expose the same commands as in Section 2, however this time the service needs to handle multiple cellular automata (all of which can be stored in memory), and each command should be expanded to include a name parameter specifying which automata to act upon. It is assumed that on encountering a new name for the first time, if the automata does not exist, it will be created anew in the empty state. The exact specification of the REST API is left upto the candidate.

## General Guidelines

* Write idiomatic F#.
* Do not optimise unnecessarily. Absolute performance is not important for this exercise; however appropriate data structures and alogorithms should be chosen given the specification
* Do not overengineer. Simplicity and elegance is favoured over unnecessary complexity.
* Platform and tools are not important. Target whichever framework you want.

## Questions

Feel free to email me: leemcsorley@gmail.com








