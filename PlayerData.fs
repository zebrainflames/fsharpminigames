module fsharptesting.PlayerData

    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework

    type PlayerData = {
        position : Vector2
        is_alive : bool
        color    : Color
        angle    : float32
        power    : float32
    }
    
    let player_colors = [
        Color.Red
        Color.Green
        Color.Blue
        Color.Purple
        Color.Orange
        Color.Indigo
        Color.Yellow
        Color.SaddleBrown
        Color.Tomato
        Color.Turquoise
        ]
    
    let create_players positions =
        let n = positions |> List.length
        let a = MathHelper.ToRadians(45.0f) 
        let p_base = {position= Vector2(0.f,0.f);  is_alive= true; angle = a; power = 100.0f; color = Color.White }
        [ for i in 0 .. (n - 1) do
            let c = player_colors.Item(i)
            let pos = positions.Item(i)
            {p_base with color = c; position = pos } ]