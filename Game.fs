module fsharptesting.Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open fsharptesting.Globals
open fsharptesting.Logic
open fsharptesting.DrawUtils




type Game1() as game =
    inherit Game()

    do game.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(game)
    let mutable device = graphics.GraphicsDevice

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    let mutable white_texture = Unchecked.defaultof<Texture2D>
    
    // Game Assets
    let background = lazy ( game.Content.Load<Texture2D> "background" )
    let foreground = lazy ( game.Content.Load<Texture2D> "foreground" )

    let draw_scenery (sb : SpriteBatch) =
        draw_fullscreen_tex(sb, background.Value)
        draw_fullscreen_tex(sb, foreground.Value)
    
    /// Member bindings & overriders below
    /// 
    override game.Initialize() =
        do base.Initialize()
        graphics.PreferredBackBufferWidth <- screen_width
        graphics.PreferredBackBufferHeight <- screen_height
        graphics.IsFullScreen <- false
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
        do game.GraphicsDevice.Clear Color.Peru
        spriteBatch.Begin()
        
        draw_scenery spriteBatch
        
        spriteBatch.End()

        base.Draw gameTime
        ()
