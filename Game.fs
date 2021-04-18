module fsharptesting.Game

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open fsharptesting.Globals
open fsharptesting.Fractal



type Game1() as game =
    inherit Game()

    do game.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(game)
    let mutable device = graphics.GraphicsDevice

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    let mutable fractal_texture = Unchecked.defaultof<Texture2D>
    let mutable fractal_pos = Unchecked.defaultof<Vector2>

    let mutable fractal = Unchecked.defaultof<Fractal>
    let mutable fractal_updated = false
    
    override game.Initialize() =
        do base.Initialize()
        graphics.PreferredBackBufferWidth <- screen_width
        graphics.PreferredBackBufferHeight <- screen_height
        graphics.IsFullScreen <- true
        graphics.ApplyChanges()
        ()

    override game.LoadContent() =
        device <- graphics.GraphicsDevice
        let f_h, f_w = 60,60
        fractal_texture <- new Texture2D(device, f_h, f_w)
        fractal_texture.SetData<Color>([| for i in 0..f_h*f_w - 1 -> if i > 1000 then Color.Peru else Color.Azure |])
        spriteBatch <- new SpriteBatch(device)

        fractal <- new_fractal screen_width screen_height Vector2.Zero device
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
        
        if not fractal_updated then
            fractal_updated <- true
            fractal <- update_fractal fractal
            Console.WriteLine("Fractal updated...")
        
        
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.Black
        spriteBatch.Begin()
        
        draw_fractal spriteBatch fractal
        //spriteBatch.Draw(fractal_texture, Vector2.Zero, Color.White)
        
        spriteBatch.End()

        base.Draw gameTime
        ()
