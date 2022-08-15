![logo](https://user-images.githubusercontent.com/11760002/184577160-674d9764-0022-4194-b4e9-d07b9103dcf5.png)

This is an extremely powerful tool for creating procedural images from scratch. You can use it to generate captchas, datasets to train neural networks or unique game mechanics - in other words, for anything your imagination can do

## Features  

- Multi-container system for processing visual entities without the need for complete redrawing
- Full randomizability of all properties of all entities - from color and font size to the strength of the distortion/blur/any effects
- A huge number of effects for post-processing images - from color correction and geometric distortion, to sharpness and pixelation modifiers
- Ability to write code with confinient [fluent interface](https://en.wikipedia.org/wiki/Fluent_interface) and the classic object-oriented approach
- Full cross-platform without being tied to any specific operating system

## Getting started

For detailed illustrated code examples, we recommend you go to the [Wiki section](https://github.com/Computr1x/ExNihilo/wiki). However, here is a sample:

```csharp
var fontFamily = new FontCollection()
    .AddSystemFonts()
    .GetByCulture(CultureInfo.CurrentCulture)
    .First();
    
Size containerSize = new(512, 256);

Container container = new(containerSize)
    .WithContainer(
        new Container(containerSize)
            .WithBackground(Color.White)
            .WithVisual(
                new Captcha()
                    .WithPoint(new Point(256,128))
                    .WithFontSize(100)
                    .WithRandomizedContent(content => {
                        content.WithLength(5);
                        content.WithCharactersSet(StringProperty.asciiUpperCase);
                    })
                    .WithRandomizedBrush(10)
                    .WithFontFamily(fontFamily)
                    .WithType(ExNihilo.Utils.VisualType.Filled)
            )
    );

new ImageSaver(
    new ImageGenerator(container)
        .WithSeedsCount(3)
        .Generate()
)
    .WithOutputPath("./")
    .CreateFolder("Results")
    .Save();
```

## Example projects

You can always find several test projects with detailed code explanations in a separate [repository](https://github.com/Computr1x/ExNihilo-Samples):

| Project | Description | Result |
|--------------------|-------------|-------------------------------------------------------------------------------------------------------------------------|
| [MathCaptcha](https://github.com/Computr1x/ExNihilo-Samples/tree/master/MathCaptcha)        | Creating a simple math captcha with a little distortion effect           |   ![3_6_-_0](https://user-images.githubusercontent.com/44768267/184555560-e031a7ed-a677-4dfb-9e5a-2106cea80a04.png)     |
| [AdvancedCaptcha](https://github.com/Computr1x/ExNihilo-Samples/tree/master/AdvancedCaptcha) | Advanced work with multiple visual objects, randomization of their parameters and post-processing effects |   ![9_DRWKU](https://user-images.githubusercontent.com/44768267/184555570-1d092b6c-73dc-4208-8186-7c211a6b9932.png)     |



# License

ExNihilo is licensed under the [Apache License, Version 2.0](https://www.apache.org/licenses/LICENSE-2.0 "Apache License, Version 2.0")
The licenses of the used libraries can be found [here](https://github.com/Computr1x/ExNihilo/blob/master/THIRD-PARTY-NOTICES.TXT)
