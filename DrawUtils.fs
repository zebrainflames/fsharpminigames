/// This module contains various drawing utilities and helpers.
/// Non optimized code but useful for testing
module fsharptesting.DrawUtils

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

printf "Module opened\n"



let draw_line (sb: SpriteBatch) (t: Texture2D) (a: Vector2) (b: Vector2) (lw: int) (c: Color) =
    let edge = b - a

    let angle =
        MathF.Atan2(float32 edge.Y, float32 edge.X)

    let len = edge.Length()

    let rect =
        new Rectangle(int a.X, int a.Y, int len, lw)

    sb.Draw(t, rect, System.Nullable(), c, float32 angle, Vector2.Zero, SpriteEffects.None, 0.0f)
    ()

let draw_rect (sb: SpriteBatch, t: Texture2D, rect: Rectangle, c: Color) =
    sb.Draw(t, rect, System.Nullable(), c, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f)
    ()

let draw_rect2 (sb : SpriteBatch) (tex : Texture2D) x y w h (c : Color) =
    sb.Draw(tex, Rectangle(x, y, w, h), c)
    ()

let draw_empty_rect (sb : SpriteBatch) (tex : Texture2D) x y w h (c : Color) =
    let rect = Rectangle(x, y, w, h)
    let lw = 1
    let top_left = Vector2(float32 x, float32 y)
    let bottom_right = Vector2(float32 x + float32 w, float32 y + float32 h)
    let top_right = Vector2(float32 x + float32 w, float32 y)
    let bottom_left = Vector2(float32 x, float32 y + float32 h)
    draw_line sb tex top_left top_right lw c
    draw_line sb tex top_right bottom_right lw c
    draw_line sb tex bottom_right bottom_left lw c
    draw_line sb tex bottom_left top_left lw c