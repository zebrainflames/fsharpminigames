module fsharptesting.Fractal

    open System
    
    open System.Numerics
    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics
    
    open fsharptesting.Globals
    
    type Fractal =
        { W: int
          H: int
          X: int
          Y: int
          Tex: Texture2D}
    
    let max_iterations = 1000
    
    let palette_1 = [Color(66,30,15);Color(25, 7, 26);Color(9, 1, 47);Color(4, 4, 73);Color(0, 7, 100);Color(12, 44, 138);Color(24, 82, 177);Color(57, 125, 209);Color(134, 181, 229);Color(211, 236, 248);Color(241, 233, 191);Color(248, 201, 95);Color(255, 170, 0);Color(204, 128, 0);Color(153, 87, 0);Color(106, 52, 3);Color.Black]
    
    let new_fractal w h (pos : Vector2) device =
        //let pixels = new Color[w * h]
        //white_texture.SetData<Color>([| Color.White |])
        // let arrayOfTenZeroes : int array = Array.zeroCreate 10
        //for i in 0..4 do
        //    printfn "Test"
        let colors : Color array = [| for _ in 0 .. (w*h - 1) -> Color.Green |]
        let mutable tex2d = new Texture2D(device, w, h)
        tex2d.SetData<Color>(colors)
        let f = { W = w; H = h;  X = int pos.X; Y = int pos.Y; Tex = tex2d}
        
        f
    
    // Map texture coordinates to Fractal space
    // TODO: zooming and offsets should probably happen later after coordinate transformations..
    let pixel_to_coord (i:int) (w:int) (h:int) =
        //var x = index % columns;
        //var y = index / columns;
        // X-axis - (-2.5, 1)
        // Y-axis - (-1, 1)
        let zoom = 1.0f
        let offset_y = 0.0f * float32 screen_height
        let offset_x = -0.0f * float32 screen_width
        let x : float32 = float32 (i % w) * 0.9f - offset_x
        let y : float32 = float32 (i / w) * zoom - offset_y
        Vector2(x, y)
    
   
    let step (zn : Complex) (c : Complex) =
        Complex.Pow(zn, 2.0) + c
    
    
    
    let map_to_mandelbrot (vec : Vector2) =
        // Map to (0, 1)
        let xm, ym = vec.X / float32 screen_width, vec.Y / float32 screen_height
        // Map to mandelbrot set ranges
        let y_f = (ym - 0.5f) * 2.0f
        let x_f = (xm - 2.5f/3.5f) * 3.5f 
        (x_f, y_f)
    
    // Iterate on texture in Mandelbrot-set space
    let iterate (pos : Vector2) : int  =
        let (x0, y0) = map_to_mandelbrot pos
        let mutable x, y = 0.0f, 0.0f
        let mutable iteration = 0
        while (x * x + y*y <= 2.0f * 2.0f && iteration < max_iterations) do
            let x_temp = x*x - y*y + x0
            y <- 2.0f * x * y + y0
            x <- x_temp
            iteration <- iteration + 1
        iteration
    let colorize iter : Color =
        // TODO: parametrize palette selection
        let palette = palette_slso8
        let i = iter % palette.Length;
        palette.[i]
    
    let update_fractal (fractal : Fractal) =
        let w, h = fractal.Tex.Width, fractal.Tex.Height
        let mutable pixels : Color array = [| for _ in 0 .. (w*h - 1) -> Color.White |]
        for i in 0..pixels.Length - 1 do
            let coord = pixel_to_coord i w h
            let escape_velocity = iterate(coord)
            pixels.[i] <- colorize escape_velocity
        fractal.Tex.SetData<Color>(pixels)
        fractal
        
        
    let draw_fractal (sprite_batch : SpriteBatch) (fractal : Fractal) =
        sprite_batch.Draw(fractal.Tex, Vector2.Zero, Color.White)
        
        ()