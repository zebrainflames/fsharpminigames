module fsharptesting.PlatformerGame
 
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System

open fsharptesting.globals
open fsharptesting.Logic


let drawLine (sb :SpriteBatch, t : Texture2D, a : Vector2, b : Vector2, lw : int) =
        let edge = b - a
        let angle = Math.Atan2(float edge.Y, float edge.X)
        let len = edge.Length()
        let rect = new Rectangle(int a.X, int a.Y, int len, lw)
        sb.Draw(t, rect, System.Nullable(), Color.Red, float32 angle, Vector2.Zero, SpriteEffects.None, 0.0f)
        ()

type Game1 () as game =
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
        t.SetData<Color>([|Color.White|])
        spriteBatch <- new SpriteBatch(device)
        
        ball <- game.Content.Load<Texture2D>("ball")
        
        ()
 
    override game.Update (gameTime) =
        ball_position <- new_position ball_position
        ()

    override game.Draw (gameTime) =
        do game.GraphicsDevice.Clear Color.Peru
        spriteBatch.Begin()
        drawLine(spriteBatch, t, Vector2(200.0f, 200.0f), Vector2(100.0f, 50.0f), 3)
        //_spriteBatch.Draw(ballTexture, new Vector2(0, 0), Color.White);
        spriteBatch.Draw(ball, ball_position, Color.White) 
        
        spriteBatch.End()
        
        base.Draw gameTime
        ()