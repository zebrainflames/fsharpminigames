module fsharptesting.MineSweeperUtils

    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Input

    // tile size
    let tile_size = 18
    let ts = 18.0f
    
    let basic_tile_rect = Rectangle(0,0,tile_size,tile_size)
    let hl_tile_rect = Rectangle(tile_size,0,tile_size,tile_size)

    let empty_tile_rect = Rectangle(tile_size*2, 0, tile_size, tile_size)
    let mine_rect = Rectangle(tile_size*3, 0, tile_size, tile_size)
    let flag_rect = Rectangle(tile_size*4, 0, tile_size, tile_size)
    let qmark_rect = Rectangle(tile_size*5, 0, tile_size, tile_size)
    
    
    // functions
    let mouse_state tile_size max_x max_y =
        let state = Mouse.GetState()
        let selected_x = MathHelper.Clamp(state.X / tile_size, 0, max_x)
        let selected_y = MathHelper.Clamp(state.Y / tile_size, 0, max_y)
        (selected_x, selected_y)
    
    let left_click () : bool =
        let state = Mouse.GetState()
        state.LeftButton = ButtonState.Pressed
    
    let right_click () : bool =
        let state = Mouse.GetState ()
        state.RightButton = ButtonState.Pressed
        
        