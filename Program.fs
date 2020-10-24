// Learn more about F# at http://fsharp.org

open fsharptesting.PlatformerGame

[<EntryPoint>]
let main argv =
    use game = new Game1()
    game.Run()
    0
