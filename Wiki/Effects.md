The library provides access to many [effects](https://github.com/Computr1x/ExNihilo/tree/master/ExNihilo/Effects) that allow you to change the color of the image, transform and distort as you wish. You can apply effects to the entire container, or apply it locally to a Visual object to edit single entity.
To demonstrate the effects, we will experiment with this image. All effect will be applied to root container. The code to generate image can be found in [effects unit tests](https://github.com/Computr1x/ExNihilo/blob/master/Tests/EffectTest.cs).  
![Test](https://user-images.githubusercontent.com/44768267/186750253-700c8ba9-ebd6-463c-baf1-e6427affcd02.png)
## Color effects
**Adaptive threshold**  
Bradley Adaptive Threshold filter [[sourse]](https://www.researchgate.net/profile/Gerhard-Roth/publication/220494200_Adaptive_Thresholding_using_the_Integral_Image/links/00b7d52b9d30a2108d000000/Adaptive-Thresholding-using-the-Integral-Image.pdf).  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Threshold | The threshold to split the image | float | [0, 1] |  

![AdaptiveThreshold_0_](https://user-images.githubusercontent.com/44768267/186757279-4c624bb6-cd0a-4c7b-a967-dec84ddac909.png)


***
**Binary threshold**  
Simple binary threshold filtering    
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Threshold | The threshold to split the image | float | [0, 1] |   

![BinaryThreshold_0_](https://user-images.githubusercontent.com/44768267/186759088-df762018-ad88-4090-8b03-28d56a460458.png)  

***
**BlackWhite**  
Black an white toning  
![BlackWhite_0_](https://user-images.githubusercontent.com/44768267/186759107-bed13f94-61de-47f9-9016-7be163e0336e.png)  
***
**Color blindness**  
Color blindness toning
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Mode|  The type of color blindness simulator | ColorBlindnessMode | enum |   

![ColorBlindness_0_](https://user-images.githubusercontent.com/44768267/186759122-de73bb48-e209-48ce-b470-f7f260b4fc6f.png)  
***
**Contrast**  
Alter contrast component
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Amount | The proportion of the conversion | float | [0, float.MaxValue] | 

![Contrast_0_](https://user-images.githubusercontent.com/44768267/186759142-ea00be84-337a-4f21-88cc-e0fe5942ff1f.png)  
***
**Dithering**  
Apply dither effect to the image [wiki](https://en.wikipedia.org/wiki/Dither)  
![Dithering_0_](https://user-images.githubusercontent.com/44768267/186759193-51c2d2f2-7c82-481f-818e-d57dfc109f0c.png)  
***
**FilterMatrix**  
Filter image color by given color matrix.
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Matrix| 5x4 matrix used for transforming the color<br/> and alpha components of an image | ColorMatrix | struct |  

![FilterMatrix_0_](https://user-images.githubusercontent.com/44768267/186759213-5f51033a-850b-4917-a562-3d32dd115901.png)  
***
**Glow**  
Applies radial glow effect.  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Radius | The radius of the glow | float | [1, float.MaxValue] |  
| Color | The color of glow | Color| struct |  

![Glow_0_](https://user-images.githubusercontent.com/44768267/186759231-02c05831-0f94-4358-951f-fd5169ee2702.png)  
***
**Grayscale**  
Applies grayscale toning effect.  
![GrayScale_0_](https://user-images.githubusercontent.com/44768267/186759240-063b1e6f-9cee-42d9-b31e-cff64d983dfc.png)  
***
**Histogram Equalization**  
Equalizes the histogram of an image to increases the contrast.  
![HistogramEqualization_0_](https://user-images.githubusercontent.com/44768267/186759276-5ec82a0d-03cb-48a6-8e32-64c91bf073d4.png)  
***
**HSB correction**  
Alter image hue, brightness and saturation channel.  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Hue | The hue shift of the image | int| [-255, 255] |  
| Saturation| The saturation shift of the image | int| [-255, 255] |  
| Brightness| The brightness shift of the image | int| [-255, 255] |  

![HSBCorrection_0_](https://user-images.githubusercontent.com/44768267/186759291-a9f6b889-e917-4e1b-a839-ac15a9ded341.png)  
***
**Invert**  
Invert the colors of the image.  
![Invert_0_](https://user-images.githubusercontent.com/44768267/186759298-9937b69a-3b06-41e6-b888-839e60a38ef8.png)  
***
**Koda chrome**  
Applies a Koda chrome camera toning effect.  
![KodaChrome_0_](https://user-images.githubusercontent.com/44768267/186759314-e651fbfc-343a-41e0-9b9a-f3c57f4134e4.png)  
***
**Lightness**  
Alter brightness component. 
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Amount| The proportion of the conversion | float | [0, float.MaxValue] |  

![Lightness_0_](https://user-images.githubusercontent.com/44768267/186759321-bd164741-4d90-48d6-9a78-22ad15b27064.png)  
***
**Lomograph**  
Applies a old Lomograph camera toning effect.  
![Lomograph_0_](https://user-images.githubusercontent.com/44768267/186759331-e9852949-4228-4751-b57c-cca8a123c41a.png)  
***
**Opacity**  
Multiplicate alpha component.  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Amount| The proportion of the conversion | float | [0, 1] |  

![Opacity_0_](https://user-images.githubusercontent.com/44768267/186759342-2e102d92-4d9c-4ef6-9fd3-7387f5e130b5.png)  
***
**Polaroid**  
Applies a polaroid toning effect.  
![Polaroid_0_](https://user-images.githubusercontent.com/44768267/186759355-e2b35de0-c9ce-41d8-b868-944a133bf4e2.png)  
***
**Sepia**  
Applies a sepia toning effect.  
![Sepia_0_](https://user-images.githubusercontent.com/44768267/186759366-be830cf5-edda-4b69-a19b-377dd16c849f.png)  
***
**Vignette**   
Applies radial vignette effect.  
![Vignette_0_](https://user-images.githubusercontent.com/44768267/186759393-5cb30410-540c-499a-8231-910dc4ba8eff.png)  



## Convultional effects
**BokehBlur**  
Applies bokeh blur effect.  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Radius| The 'radius' value representing the size </br>of the area to sample | int | [1, int.MaxValue] |  
| Components| The 'components' value representing the </br>number of kernels to use to approximate the bokeh effect | float | [1, int.MaxValue] |  
| Gamma| The gamma highlight factor to use to emphasize bright spots in the source image | float | [1, float.MaxValue] |  

![BokehBlur_0_](https://user-images.githubusercontent.com/44768267/186985373-b25379bf-7459-46cb-93de-7fcd2b3a759f.png)

***
**BoxBlur**  
Applies box blur effect. [wiki](https://en.wikipedia.org/wiki/Bokeh)   
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Radius| The 'radius' value representing the size </br>of the area to sample | int | [1, int.MaxValue] |  

![BoxBlur_0_](https://user-images.githubusercontent.com/44768267/186985406-0fe35bca-fef3-4ab9-834a-26bbc42382b3.png)  

***
**Crystallize**  
Applies crystallization effect.  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| CrystalsCount| The number of crystals into which </br>the image will be divided | int | [1, int.MaxValue] |  

![Crystallize_0_](https://user-images.githubusercontent.com/44768267/186985588-f41f56fa-41da-408b-9e44-5fed1c455e98.png)

***
**GaussianBlur**  
Applies gaussian blue effect. [wiki](https://en.wikipedia.org/wiki/Gaussian_blur)   
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Sigma| The 'sigma' value representing </br>the weight of the blur | float | [0, 1] |  

![GaussianBlur_0_](https://user-images.githubusercontent.com/44768267/186985626-f594767e-928c-4798-ba7c-bedee579485b.png)

***
**GaussianSharpen**  
Applies gaussian sharp effect.  
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Sigma| The 'sigma' value representing the weight of the blur | float | [0, 1] |  

![GaussianSharpen_0_](https://user-images.githubusercontent.com/44768267/186985644-117b045c-cb03-49e4-96f4-3147916e2171.png)

***
**OilPaint**  
Alters the colors of the image recreating an oil painting effect.   
| Property | Description | Type | Value |
|----------|-------------|------|------|
| Levels| The number of intensity levels. </br>Higher values result in a broader range of color </br>intensities forming part of the result image | int | [1, int.MaxValue] |
| BrushSize| The number of neighboring pixels used in </br>calculating each individual pixel value | int | [1, int.MaxValue] |  

![OilPaint_0_](https://user-images.githubusercontent.com/44768267/186985667-edb85728-86b7-4db5-aff8-0aa9e95e8d87.png)

***
**Pixelate**  
Pixelate image by given pixel size.   
| Property | Description | Type | Value |
|----------|-------------|------|------|
| PixelSize| The size of the pixels | int | [1, int.MaxValue] |  

![Pixelate_0_](https://user-images.githubusercontent.com/44768267/186985687-ad932c1d-c751-4726-9544-48c23d0e98fb.png)

***
**Quantize**  
Applies quantization effect.   

![Quantize_0_](https://user-images.githubusercontent.com/44768267/186985708-6d504e79-9818-4fb0-b919-229c2b69c055.png)

## Distortional effects
1
## Noise effects
1
## Transform effects
1