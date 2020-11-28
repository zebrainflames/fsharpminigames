module fsharptesting.Game

open System.Numerics
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open fsharptesting.Cell
open fsharptesting.Globals
open fsharptesting.Logic
open fsharptesting.DrawUtils
open fsharptesting.PlayerData
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
    
    let draw_cell pos cell =
        let rect =
            match cell with
            | Empty -> empty_tile_rect
            | Neighbour n -> num_rect n
            | Bomb -> mine_rect
            | CoveredBomb -> basic_tile_rect
            | Closed -> basic_tile_rect
            | _ -> hl_tile_rect
        draw_with_rect pos rect
        
    
    let draw_board () =
        for x in 0..grid_width do
            for y in 0..grid_height do
                let cell = game_board.[x, y]
                let pos = Vector2(float32 (x*tile_size), float32 (y*tile_size))
                draw_cell pos cell
        ()
        
    
    let mouse_coord () = mouse_state tile_size grid_width grid_height
    
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
        graphics.ApplyChanges()
        
        ()

    override game.LoadContent() =
        device <- graphics.GraphicsDevice
        white_texture <- new Texture2D(device, 1, 1)
        white_texture.SetData<Color>([| Color.White |])
        spriteBatch <- new SpriteBatch(device)
    
    
    override game.Update(gameTime) =
        let (m_x, m_y) = mouse_coord()
        grid_pos_string <- sprintf "Pos: (%d, %d)" m_x m_y
        
        if mouse_pressed() then
            //game_board.[m_x, m_y] <- Empty
            open_cell game_board m_x m_y
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.SlateGray
        spriteBatch.Begin()
        // Start of draw calls from the actual game
        
        //draw_board spriteBatch
        let (m_x, m_y) = mouse_coord()
        let mouse_pos = Vector2(float32(m_x)*ts, float32(m_y)*ts)
        (*
        for x in 0.f .. float32 grid_width do
            for y in 0.f .. float32 grid_height do
                let pos = Vector2(x*ts, y*ts)
                if m_x = int x && m_y = int y then
                    if mouse_pressed() then
                        draw_empty mouse_pos
                    else
                        draw_hl_tile mouse_pos
                else
                    draw_tile pos*)
        draw_board()
        
        // Draw text & other UI
        //spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);
        do spriteBatch.DrawString(font.Value, grid_pos_string, Vector2.Zero, Color.Black)
            
        
        // Sprite batch ends, drawing stops
        spriteBatch.End()
        base.Draw gameTime
        ()
