module fsharptesting.Cell
    open fsharptesting.Matrix

    type Cell =
        | Empty
        | Bomb
        | CoveredBomb
        | Neighbour of int
        | Closed
        
        
    type Board = { cells : Matrix<Cell> } with
        member this.At x y = this.cells.Item(x, y)
        member this.Set x y c = this.cells.[x, y] <- c
    
    // TODO: generate board properly
    let create_board w h =
        let matrix = Matrix<Cell>(w+1, h+1)
        for i in 0..(w) do
                for j in 0..(h) do
                    matrix.[i, j] <- Closed
        matrix