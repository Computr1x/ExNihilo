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

You can always find several test projects with detailed code explanations in a direcory [examples](https://github.com/Computr1x/ExNihilo/tree/master/Examples/):

| Project | Description | Result |
|--------------------|-------------|-------------------------------------------------------------------------------------------------------------------------|
| [SimpleCaptcha](https://github.com/Computr1x/ExNihilo/tree/master/Examples/SimpleCaptcha)      |  The example shows basic canvas operations such as working with layers, text, generating and saving an image.           |   ![1_GCMUR](https://user-images.githubusercontent.com/44768267/184554245-57633e01-b30a-4669-87f9-c59886f725c6.png)     |
| [AdvancedCaptcha](https://github.com/Computr1x/ExNihilo/tree/master/Examples/AdvancedCaptcha)    | Advanced work with multiple visual objects, randomization of their parameters and post-processing effects            |   ![3_SZCRR](https://user-images.githubusercontent.com/44768267/184554299-01e5bfd0-a765-4c38-8800-35b2f1d93f3d.png)     |
| [MathCaptcha](https://github.com/Computr1x/ExNihilo/tree/master/Examples/MathCaptcha)        | Creating a simple math captcha with a little distortion effect            |   ![2_4_+_3](https://user-images.githubusercontent.com/44768267/184554306-297c0294-a13e-4fde-a775-061a1eee2b0f.png)     |
| [TwoLanguageCaptcha](https://github.com/Computr1x/ExNihilo/tree/master/Examples/TwoLanguageCaptcha) |  This example shows how to create a captcha with different fonts for rendering text as text in two languages.           |   ![0_TOFOX](https://user-images.githubusercontent.com/44768267/184554323-09ac4649-4612-418c-b145-2438c5beb59f.png)     |
| [WPF](https://github.com/Computr1x/ExNihilo/tree/master/Examples/WPF) |  This example shows how to create and display image in WPF project.           |   ![wpf](https://user-images.githubusercontent.com/44768267/184982218-041de533-59bb-4278-bff8-12f5a96f3eb9.PNG)|


# License

ExNihilo is licensed under the [Apache License, Version 2.0](https://www.apache.org/licenses/LICENSE-2.0 "Apache License, Version 2.0")
The licenses of the used libraries can be found [here](https://github.com/Computr1x/ExNihilo/blob/master/THIRD-PARTY-NOTICES.TXT)
