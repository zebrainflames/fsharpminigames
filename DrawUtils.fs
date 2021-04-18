/// This module contains various drawing utilities and helpers.
/// Non optimized code but useful for testing
module fsharptesting.DrawUtils

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics


let drawLine (sb: SpriteBatch, t: Texture2D, a: Vector2, b: Vector2, lw: int, c: Color) =
    let edge = b - a

    let angle =
        MathF.Atan2(float32 edge.Y, float32 edge.X)

    let len = edge.Length()

    let rect =
        new Rectangle(int a.X, int a.Y, int len, lw)

    sb.Draw(t, rect, System.Nullable(), c, float32 angle, Vector2.Zero, SpriteEffects.None, 0.0f)
    ()

let drawRect (sb: SpriteBatch, t: Texture2D, rect: Rectangle, c: Color) =
    sb.Draw(t, rect, System.Nullable(), c, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f)
    ()
