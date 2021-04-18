module fsharptesting.Game

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open fsharptesting.Globals


type Game1() as game =
    inherit Game()

    do game.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(game)
    let mutable device = graphics.GraphicsDevice

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    
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
        
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.Black
        spriteBatch.Begin()
        
        //draw_fractal spriteBatch fractal
        //spriteBatch.Draw(fractal_texture, Vector2.Zero, Color.White)
        
        spriteBatch.End()

        base.Draw gameTime
        ()
