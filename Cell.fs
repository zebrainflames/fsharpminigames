module fsharptesting.Cell
    open System
    open Microsoft.Xna.Framework
    open fsharptesting.Matrix

    type Cell =
        | Empty
        | Bomb
        | CoveredBomb
        // TODO: neighbour should be tuple of whether the data is unrevealed or not?
        | CoveredNeighbour of int
        | Neighbour of int
        | Closed
        | Flag
        
    
    type Board = { cells : Matrix<Cell> } with
        member this.At x y = this.cells.Item(x, y)
        member this.Set x y c = this.cells.[x, y] <- c
            
    
    let rand = Random()
    
    let random_cell_type () =
        let n = rand.Next(0, 101)
        if n <= 10 then
            CoveredBomb
        else
            Closed
    
    
    
    //TODO: trigger neighbour data update when bomb is added
    // TODO: currently this doesn't work correctly. Just assumes all are neighbours. FIX.
    let rec update_neighbours (matrix : Matrix<Cell>) x y orig_x orig_y max_x max_y =
        let cell =
            match matrix.[x, y] with
            | Empty -> CoveredNeighbour(1)
            | CoveredNeighbour cn -> CoveredNeighbour(cn + 1)
            | Neighbour n -> Neighbour n
            | CoveredBomb -> CoveredBomb
            | Bomb -> Bomb
            | Flag -> Flag
            | Closed -> CoveredNeighbour(1)
            //| _ -> failwith "todo"
        matrix.[x, y] <- cell            
        for i in (x-1)..(x+1) do
            for j in (y-1)..(y+1) do
                if i >= 0 && i <= max_x && j >= 0 && j <= max_y && i <> orig_x && j <> orig_y then
                    update_neighbours matrix i j orig_x orig_y max_x max_y
        ()
    
    let open_cell (matrix : Matrix<Cell>) x y =
        let cell =
            match matrix.[x, y] with
            | Empty -> Empty
            | Bomb -> Bomb
            | CoveredBomb -> Bomb
            | CoveredNeighbour n -> Neighbour n
            | Neighbour n -> Neighbour n
            | Closed -> Empty
            | Flag -> Flag
        matrix.[x, y] <- cell
    
    let create_board w h =
        let matrix = Matrix<Cell>(w+1, h+1)
        for i in 0..(w) do
                for j in 0..(h) do
                    let cell = random_cell_type()
                    //if cell = CoveredBomb then
                        //update_neighbours matrix i j i j w h
                    matrix.[i, j] <- cell
        //do update_neighbours matrix 0 0 0 0 w h
        matrix