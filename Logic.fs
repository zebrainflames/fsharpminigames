module fsharptesting.Logic

open Microsoft.Xna.Framework
open fsharptesting.Globals

let move_speed = 2.0f


let clamp coord max =
    if coord > max then max
    elif coord < 0.0f then 0.0f
    else coord

let new_position (pos: Vector2) =
    let x = clamp pos.X (float32 screen_width)
    let y = clamp pos.Y (float32 screen_height)
    Vector2(x, y) + Vector2.One * move_speed
