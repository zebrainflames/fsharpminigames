// TODO: This module name is stupi :--D
module fsharptesting.Logic

open Microsoft.Xna.Framework
open fsharptesting.Globals

let clamp coord max =
    if coord > max then max
    elif coord < 0.0f then 0.0f
    else coord
