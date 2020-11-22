module fsharptesting.Game

open System.Numerics
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Graphics
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
    let ts = 18.0f
    let grid_width = 19
    let grid_height = 14
      
    let null_rect = System.Nullable<Rectangle>()
    
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
        
    let draw_board (sb : SpriteBatch) =
        let ts = 18.0f
        draw_tile Vector2.Zero
        draw_hl_tile(Vector2(ts, 0.0f))
        draw_mine (Vector2(ts*2.f, 0.0f))
        draw_flag (Vector2(ts*3f, 0.0f))
        draw_qmark (Vector2(ts * 4f, 0f))
    
    /// Member bindings & overriders below
    /// 
    override game.Initialize() =
        do base.Initialize()
        graphics.PreferredBackBufferWidth <- screen_width
        graphics.PreferredBackBufferHeight <- screen_height
        graphics.IsFullScreen <- false
        game.Window.Title <- "Minesweeper"
        graphics.ApplyChanges()
        ()

    override game.LoadContent() =
        device <- graphics.GraphicsDevice
        white_texture <- new Texture2D(device, 1, 1)
        white_texture.SetData<Color>([| Color.White |])
        spriteBatch <- new SpriteBatch(device)

        
        ()

    override game.Update(gameTime) =
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.Aqua
        spriteBatch.Begin()
        // Start of draw calls from the actual game
        
        draw_board spriteBatch

        let start_pos = Vector2(0.f, ts)
        for i in 1 .. 8 do
            let pos = start_pos + Vector2(float32(i - 1)*ts, 0f)
            do draw_number pos i
            
        
        // Sprite batch ends, drawing stops
        spriteBatch.End()
        base.Draw gameTime
        ()
