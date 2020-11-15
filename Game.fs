module fsharptesting.Game

open System.Numerics
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open fsharptesting.Globals
open fsharptesting.Logic
open fsharptesting.DrawUtils
open fsharptesting.PlayerData




type Game1() as game =
    inherit Game()
    // framework config etc.
    do game.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(game)
    let mutable device = graphics.GraphicsDevice

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    let mutable white_texture = Unchecked.defaultof<Texture2D>
    
    // Game Assets
    let background = lazy ( game.Content.Load<Texture2D> "background" )
    let foreground = lazy ( game.Content.Load<Texture2D> "foreground" )
    let cannon = lazy ( game.Content.Load<Texture2D> "cannon" )
    let carriage = lazy ( game.Content.Load<Texture2D> "carriage" )
    
    let player_scale = lazy ( 40.0f / float32 carriage.Value.Width )
    
    // game logic
    let players = create_players [ Vector2(100.f,193.f); Vector2(200.f,212.f); Vector2(300.f,361.f); Vector2(400.f,164.f)]
    let number_of_players = players |> List.length
    
    let null_rect = System.Nullable<Rectangle>()
    
    let draw_player (sb : SpriteBatch) ( pd : PlayerData) =
        let scale = player_scale.Value
        do sb.Draw(carriage.Value,
                   pd.position, null_rect, pd.color, 0.0f, Vector2(0.f, float32 carriage.Value.Height),
                   scale, SpriteEffects.None, 0.0f)
        
        let cannon_origin = Vector2(11.f, 50.f)
        let cannon_offset = Vector2(20.f, -10.f)
        do sb.Draw(cannon.Value, pd.position + cannon_offset, null_rect, pd.color, pd.angle, cannon_origin,
                   scale, SpriteEffects.None, 1.0f)
    
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
        game.Window.Title <- "BombGophers"
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
        
        List.filter (fun (p : PlayerData) -> p.is_alive) players
        |> List.iter (fun p -> draw_player spriteBatch p)
        
        //for p in alive_players do
        //    draw_player spriteBatch p
        
        spriteBatch.End()

        base.Draw gameTime
        ()
