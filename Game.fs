module fsharptesting.Game

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Microsoft.Xna.Framework.Input
open fsharptesting.Globals
open fsharptesting.DrawUtils

type Game1() as game =
    inherit Game()

    do game.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(game)
    let mutable device = graphics.GraphicsDevice

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    let mutable empty_tex = Unchecked.defaultof<Texture2D>

    // Gameplay related vars
    let mutable x_pos = 100.0f
    
    let get_empty_tex (sb : SpriteBatch) =
        if empty_tex = null then
            printfn "Creating empty texture..."
            empty_tex <- new Texture2D(sb.GraphicsDevice, 1, 1)
            empty_tex.SetData([|Color.White|])
        empty_tex

    
    override game.Initialize() =
        do base.Initialize()
        graphics.PreferredBackBufferWidth <- screen_width
        graphics.PreferredBackBufferHeight <- screen_height
        //graphics.IsFullScreen <- true
        graphics.ApplyChanges()
        ()

    override game.LoadContent() =
        device <- graphics.GraphicsDevice
        spriteBatch <- new SpriteBatch(device)

        //fractal <- new_fractal screen_width screen_height Vector2.Zero device
        ()

    override game.Update(gameTime) =
        let delta = float32 gameTime.ElapsedGameTime.TotalSeconds;
        (*
        // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();
            
            // If they hit esc, exit
            if (state.IsKeyDown(Keys.Escape))
                Exit();
        *)
        let state = Keyboard.GetState()
        if state.IsKeyDown Keys.Escape then
            game.Exit()
        let d_x =
            if state.IsKeyDown Keys.A && state.IsKeyUp Keys.D then
                -60.0f
            elif state.IsKeyDown Keys.D && state.IsKeyUp Keys.A then
                60.0f
            else
                0.0f
        x_pos <- x_pos + d_x * delta
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.Black
        spriteBatch.Begin()
        let tex = get_empty_tex spriteBatch
        do draw_empty_rect spriteBatch tex (int x_pos) 50 200 150 Color.White
        
        spriteBatch.End()

        base.Draw gameTime
        ()
