module fsharptesting.Cell
    open System
    open Microsoft.Xna.Framework
    open fsharptesting.Matrix

    type Cell =
        | Empty
        | Bomb
        | CoveredBomb
        | Closed
        | Flag
        
    
    type Board = { cells : Matrix<Cell> } with
        member this.At x y = this.cells.Item(x, y)
        member this.Set x y c = this.cells.[x, y] <- c
            
    
    let rand = Random()
    
    let random_cell_type () =
        let n = rand.Next(0, 101)
        if n <= 3 then
            CoveredBomb
        else
            Closed
    
    
    
    let rec update_neighbours (matrix : Matrix<Cell>) x y orig_x orig_y max_x max_y =
        let cell =
            match matrix.[x, y] with
            | Empty -> Empty
            | CoveredBomb -> CoveredBomb
            | Bomb -> Bomb
            | Flag -> Flag
            | Closed -> Empty
            //| _ -> failwith "todo"
        matrix.[x, y] <- cell            
        for i in (x-1)..(x+1) do
            for j in (y-1)..(y+1) do
                if i >= 0 && i <= max_x && j >= 0 && j <= max_y && i <> orig_x && j <> orig_y then
                    update_neighbours matrix i j orig_x orig_y max_x max_y
        ()
    
    
    
    // TODO: simplify...
    let neighbour_bombs (matrix : Matrix<Cell>) x y max_x max_y =
        let mutable n = 0
        for dx in -1..1 do
            for dy in -1..1 do
                if dx <> 0 || dy <> 0 then
                    if x + dx >= 0 && x + dx <= max_x && y + dy >= 0 && y + dy <= max_y then
                        let cell = matrix.[x + dx, y + dy]
                        if cell = Bomb || cell = CoveredBomb then
                            n <- n + 1
        n
        
    
    let open_cell (matrix : Matrix<Cell>) x y max_x max_y =
        let (cell, recurse) =
            match matrix.[x, y] with
            | Empty -> (Empty, false)
            | Bomb -> (Bomb, false)
            | CoveredBomb -> (Bomb, false)
            | Closed -> (Empty, true)
            | Flag -> (Flag, false)
        matrix.[x, y] <- cell
        if recurse && neighbour_bombs matrix x y max_x max_y = 0 then
            true
        else
            false
    
    let create_board w h =
        let matrix = Matrix<Cell>(w+1, h+1)
        for i in 0..(w) do
                for j in 0..(h) do
                    let cell = random_cell_type()
                    matrix.[i, j] <- cell
        matrix