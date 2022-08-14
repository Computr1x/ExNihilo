<h1 align="center">ExNihilo</h1>  

ExNihilo - modern, powerfull and flexible random image generation library.  

## Featrures  
- The layer system allows you to add objects and effects without changing other entities.
- The parameters of objects and effects can be set randomly, which allows you to generate unique images that are great for captcha.
- Fluent interface lets you create images in one line of code if you want.
- A large number of effects will allow you to create an image the way you want.
- The library is cross-platform, which will allow you to work with it regardless of your operating system.

# Documentation
All documentation you can find on [Wiki](https://github.com/Computr1x/TCG/wiki) page.

# Example  
``` cs
var fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();
Size canvasSize = new(512, 256);
Canvas canvas = new Canvas(canvasSize)
    .WithLayer(
        new Layer(canvasSize)
            .WithBackground(Color.White)
            .WithDrawable(
                new Captcha()
                    .WithPoint(new Point(256,128))
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    { content.WithLength(5); content.WithCharactersSet(StringParameter.asciiUpperCase);})
                    .WithRandomizedBrush(10)
                    .WithFontFamily(fontFamily)
                    .WithType(ExNihilo.Base.Utils.DrawableType.Filled)));

new ImageSaver(new ImageGenerator(canvas).WithSeedsCount(3).Generate()).
    WithOutputPath("./").CreateFolder("Results").Save();
```

# Samples
Few samples here and more you can find in samples [repository](https://github.com/Computr1x/ExNihilo-Samples).  
| Example            | Description | Result |
|--------------------|-------------|-------------------------------------------------------------------------------------------------------------------------|
| [AdvancedCaptcha](https://github.com/Computr1x/ExNihilo-Samples/tree/master/AdvancedCaptcha)    | An advanced example shows working with effects, parameter randomization, and a variety of visual objects.            |   ![9_DRWKU](https://user-images.githubusercontent.com/44768267/184555570-1d092b6c-73dc-4208-8186-7c211a6b9932.png)     |
| [MathCaptcha](https://github.com/Computr1x/ExNihilo-Samples/tree/master/MathCaptcha)        | This example shows how to create a math captcha with little distortion.            |   ![3_6_-_0](https://user-images.githubusercontent.com/44768267/184555560-e031a7ed-a677-4dfb-9e5a-2106cea80a04.png)     |




# Licence
ExNihilo is licensed under the [Apache License, Version 2.0](https://www.apache.org/licenses/LICENSE-2.0 "Apache License, Version 2.0")
The licenses of the used libraries can be found [here](https://github.com/Computr1x/TCG/blob/master/THIRD-PARTY-NOTICES.TXT).
