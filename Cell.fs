module fsharptesting.Cell
    open fsharptesting.Matrix

    type Cell =
        | Empty
        | Bomb
        | CoveredBomb
        | Neighbour of int
        
        
    type Board = { cells : Matrix<Cell> } with
        member this.At x y = this.cells.Item(x, y)
        member this.Set x y c = this.cells.[x, y] <- c
    
    // TODO: generate board properly
    let create_board w h =
        let matrix = Matrix<Cell>(w, h)
        for i in 0..(w-1) do
                for j in 0..(h-1) do
                    matrix.[i, j] <- Empty
        matrix