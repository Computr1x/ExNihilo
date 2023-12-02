<img src="https://user-images.githubusercontent.com/11760002/184577160-674d9764-0022-4194-b4e9-d07b9103dcf5.png">

[![Build status](https://github.com/Computr1x/ExNihilo/workflows/Build/badge.svg)](https://github.com/Computr1x/ExNihilo/actions?query=workflow%3ABuild)
[![Latest release](https://img.shields.io/github/release/Computr1x/ExNihilo.svg)
[![NuGet](https://img.shields.io/nuget/dt/ExNihilo.svg)](https://www.nuget.org/packages/ExNihilo)
[![License](http://img.shields.io/:license-apache-blue.svg)](http://www.apache.org/licenses/LICENSE-2.0.html)

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
                new Text()
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
        .Generate())
.WithOutputPath("./")
.CreateFolder("Results")
.Save();
```

## Example projects

You can always find several test projects with detailed code explanations in a directory [/examples](https://github.com/Computr1x/ExNihilo/tree/master/Examples/):

| Project | Description | Result |
|--------------------|-------------|-------------------------------------------------------------------------------------------------------------------------|
| [Simple](https://github.com/Computr1x/ExNihilo/tree/master/Examples/Simple)      |  The example shows basic container operations such as working with containers, text, generating and saving an image.           |   ![1_GCMUR](https://user-images.githubusercontent.com/44768267/184554245-57633e01-b30a-4669-87f9-c59886f725c6.png)     |
| [AdvancedCaptcha](https://github.com/Computr1x/ExNihilo/tree/master/Examples/AdvancedCaptcha)    | Advanced work with multiple visual objects, randomization of their parameters and post-processing effects.            |   ![3_SZCRR](https://user-images.githubusercontent.com/44768267/184554299-01e5bfd0-a765-4c38-8800-35b2f1d93f3d.png)     |
| [TwoLanguage](https://github.com/Computr1x/ExNihilo/tree/master/Examples/TwoLanguage) |  This example shows how to create a image with different fonts for rendering text in two languages.           |   ![0_TOFOX](https://user-images.githubusercontent.com/44768267/184554323-09ac4649-4612-418c-b145-2438c5beb59f.png)     |
| [WPF](https://github.com/Computr1x/ExNihilo/tree/master/Examples/WPF) |  This example shows how to create and display image in WPF project.           |    ![wpf](https://user-images.githubusercontent.com/44768267/186938211-42dc5871-faa6-404b-b03c-4039d39f10be.PNG)|


# License

ExNihilo is licensed under the [Apache License, Version 2.0](https://www.apache.org/licenses/LICENSE-2.0 "Apache License, Version 2.0")
The licenses of the used libraries can be found [here](https://github.com/Computr1x/ExNihilo/blob/master/THIRD-PARTY-NOTICES.TXT)
