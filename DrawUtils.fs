/// This module contains various drawing utilities and helpers.
/// Non optimized code but useful for testing
module fsharptesting.DrawUtils

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open fsharptesting.Globals


let drawLine (sb: SpriteBatch, t: Texture2D, a: Vector2, b: Vector2, lw: int, c: Color) =
    let edge = b - a

    let angle =
        MathF.Atan2(float32 edge.Y, float32 edge.X)

    let len = edge.Length()

    let rect =
        new Rectangle(int a.X, int a.Y, int len, lw)

    sb.Draw(t, rect, System.Nullable(), c, float32 angle, Vector2.Zero, SpriteEffects.None, 0.0f)
    ()

let drawRect (sb: SpriteBatch, t: Texture2D, r: Rectangle, c: Color) =
    let b = float32 r.Bottom
    let t = float32 r.Top
    let l = float32 r.Left
    let r = float32 r.Right

    ()
    
let draw_fullscreen_tex(sb : SpriteBatch, t : Texture2D) =
    let r = Rectangle(0 , 0, screen_width, screen_height)
    do sb.Draw(t, r, Color.White)
