module fsharptesting.Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open fsharptesting.Cell
open fsharptesting.DrawUtils
open fsharptesting.MineSweeperUtils




type Game1() as game =
    inherit Game()
    // framework config etc.
    do game.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(game)
    let mutable device = graphics.GraphicsDevice

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    let mutable white_texture = Unchecked.defaultof<Texture2D>
    
    // Game Assets
    let ms_texture = lazy ( game.Content.Load<Texture2D> "minesweeper/mw_assets")
    let font = lazy ( game.Content.Load<SpriteFont> "myFont")
    let ts = 18.0f
    let grid_width = 19
    let grid_height = 14
    let mutable (grid_pos_string : string) = sprintf "Pos: (%d, %d)" 1 1
    let null_rect = System.Nullable<Rectangle>()
    
    let mutable game_board = create_board grid_width grid_height
    
    let mutable won = false
    
    let bomb_locations =
        let mutable ctr = 0
        let locs = [
            for i in 0..grid_width ->
                [for j in 0..grid_height ->
                    if game_board.[i,j] = CoveredBomb then
                        ctr <- ctr + 1
                        Some (Vector2(float32 i, float32 j))
                    else
                        None
                        ]]
        locs |> List.concat |> List.choose id 
    
    let is_bomb_location pos =
        List.contains pos bomb_locations
    
    let mutable flags : Vector2 list = []
    
    let mutable right_click_pressed = false
    
    let num_rect n =
        let y = tile_size
        let x = (n - 1) * tile_size
        Rectangle(x, y, tile_size, tile_size)
    
    let draw_number pos (num : int) =
        let y = tile_size
        let x = (num - 1) * tile_size
        let r = Rectangle(x, y, tile_size, tile_size)
        draw_from_sheet(spriteBatch, ms_texture.Value, pos, r)
        ()
    
    let draw_tile pos = draw_from_sheet(spriteBatch, ms_texture.Value, pos, basic_tile_rect)
    let draw_hl_tile pos = draw_from_sheet(spriteBatch, ms_texture.Value, pos, hl_tile_rect)
    let draw_mine pos = draw_from_sheet(spriteBatch, ms_texture.Value, pos, mine_rect)
    let draw_flag pos = draw_from_sheet(spriteBatch, ms_texture.Value, pos, flag_rect)
    let draw_qmark pos = draw_from_sheet(spriteBatch, ms_texture.Value, pos, qmark_rect)
    let draw_empty pos = draw_from_sheet(spriteBatch, ms_texture.Value, pos, empty_tile_rect)
    
    let draw_with_rect pos rect = draw_from_sheet(spriteBatch, ms_texture.Value, pos, rect)
    
    
    let empty_or_num_rect matrix x y =
        let num = neighbour_bombs matrix x y grid_width grid_height
        if num = 0 then
            empty_tile_rect
        else
            num_rect num
    
    let draw_cell x y cell =
        let rect =
            match cell with
            | Empty -> empty_or_num_rect game_board x y
            | Bomb -> mine_rect
            | CoveredBomb -> basic_tile_rect
            | Closed -> basic_tile_rect
            | _ -> hl_tile_rect
        let pos = Vector2(float32 (x*tile_size), float32 (y*tile_size))
        draw_with_rect pos rect
        
    
    let draw_board () =
        for x in 0..grid_width do
            for y in 0..grid_height do
                let cell = game_board.[x, y]
                
                draw_cell x y cell
        ()
    
    let draw_flags () =
        for f in flags do
            let pos = Vector2(f.X * (float32 tile_size), f.Y * (float32 tile_size))
            draw_flag pos
        ()
    
    let mouse_coord () = mouse_state tile_size grid_width grid_height
    
    let rec cascade_empty_cells m_x m_y =
        let recurse = open_cell game_board m_x m_y grid_width grid_height
        if not recurse then
            ()          
        else
            for dx in -1..1 do
                for dy in -1..1 do
                    if dx <> 0 || dy <> 0 then
                        if m_x + dx >= 0 && m_x + dx <= grid_width && m_y + dy >= 0 && m_y + dy <= grid_height then
                            cascade_empty_cells (m_x + dx) (m_y + dy)
    
    let check_flag m_x m_y =
        let rec remove_first pred lst =
            match lst with
            | h::t when pred h -> t
            | h::t -> h::remove_first pred t
            | _ -> []
        
        let vec = Vector2(float32 m_x, float32 m_y)
        if flags |> List.contains vec then
            printf "Flag already set at (%d, %d), removing\n" m_x m_y
            flags <- flags |> remove_first (fun flag -> flag = vec)
        else
            flags <- vec::flags
            printf "Added flag at (%d, %d)\n" m_x m_y
            
        if is_bomb_location vec then
            printf "Clicked location is bomb location\n"
        else
            printf "clicked location is not bomb location\n"
        ()
    
    let flags_missing () =
        bomb_locations |> List.map (fun pos -> flags |> List.contains pos) |> List.contains false
    
    /// Member bindings & overriders below
    /// 
    override game.Initialize() =
        do base.Initialize()
        let preferred_width = grid_width * tile_size + tile_size
        let preferred_height = grid_height * tile_size + tile_size
        graphics.PreferredBackBufferWidth <- preferred_width
        graphics.PreferredBackBufferHeight <- preferred_height
        graphics.IsFullScreen <- false
        game.Window.Title <- "Minesweeper"
        game.IsMouseVisible <- true
        graphics.ApplyChanges()
        
        //bomb_locations ()
        
        ()

    override game.LoadContent() =
        device <- graphics.GraphicsDevice
        white_texture <- new Texture2D(device, 1, 1)
        white_texture.SetData<Color>([| Color.White |])
        spriteBatch <- new SpriteBatch(device)
    
    
    override game.Update(gameTime) =
        let (m_x, m_y) = mouse_coord()
        grid_pos_string <- sprintf "Pos: (%d, %d)" m_x m_y
        
        if left_click() then
            cascade_empty_cells m_x m_y
        
        let rc = right_click()
        if rc && not right_click_pressed then
            check_flag m_x m_y
            right_click_pressed <- true
        elif not rc then
            right_click_pressed <- false
        
        let bombs_not_flagged = flags_missing ()
        if not bombs_not_flagged && not won then
            printfn "All bombs successfully flagged!"
            won <- true
        
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.SlateGray
        spriteBatch.Begin()
        // Start of draw calls from the actual game
        
        //draw_board spriteBatch
        let (m_x, m_y) = mouse_coord()
        let mouse_pos = Vector2(float32(m_x)*ts, float32(m_y)*ts)

        draw_board()

        draw_flags ()
                
        // Draw text & other UI
        //spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);
        do spriteBatch.DrawString(font.Value, grid_pos_string, Vector2.Zero, Color.Black)
            
        
        // Sprite batch ends, drawing stops
        spriteBatch.End()
        base.Draw gameTime
        ()
