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

    let mutable t = Unchecked.defaultof<Texture2D>
    let mutable ball = Unchecked.defaultof<Texture2D>
    let mutable ball_position = Unchecked.defaultof<Vector2>


    override game.Initialize() =
        do base.Initialize()
        graphics.PreferredBackBufferWidth <- screen_width
        graphics.PreferredBackBufferHeight <- screen_height
        graphics.IsFullScreen <- false
        graphics.ApplyChanges()
        ()

    override game.LoadContent() =
        device <- graphics.GraphicsDevice
        t <- new Texture2D(device, 1, 1)
        t.SetData<Color>([| Color.White |])
        spriteBatch <- new SpriteBatch(device)

        ball <- game.Content.Load<Texture2D>("ball")

        ()

    override game.Update(gameTime) =
        ball_position <- new_position ball_position
        ()

    override game.Draw(gameTime) =
        do game.GraphicsDevice.Clear Color.Peru
        spriteBatch.Begin()
        drawLine (spriteBatch, t, Vector2(200.0f, 200.0f), Vector2(100.0f, 50.0f), 3, Color.Red)
        //_spriteBatch.Draw(ballTexture, new Vector2(0, 0), Color.White);
        spriteBatch.Draw(ball, ball_position, Color.White)

        spriteBatch.End()

        base.Draw gameTime
        ()
