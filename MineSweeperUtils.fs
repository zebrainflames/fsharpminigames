module fsharptesting.MineSweeperUtils

    open Microsoft.Xna.Framework

    // tile size
    let tile_size = 19
    
    let basic_tile_rect = Rectangle(0,0,tile_size,tile_size)
    let hl_tile_rect = Rectangle(tile_size,0,tile_size,tile_size)

    let empty_tile_rect = Rectangle(tile_size*2, 0, tile_size, tile_size)
    let mine_rect = Rectangle(tile_size*3, 0, tile_size, tile_size)
    let flag_rect = Rectangle(tile_size*4, 0, tile_size, tile_size)
    let qmark_rect = Rectangle(tile_size*5, 0, tile_size, tile_size)