using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Effects;

namespace TCG.Base.Interfaces
{
    public interface IRandomEffectGenerator
    {
        public void RandomizeAdaptiveThreshold(AdaptiveThreshold effect);
        public void RandomizeBinaryThreshold(BinaryThreshold effect);
        public void RandomizeBlackWhite(BlackWhite effect);
        public void RandomizeColorBlindness(ColorBlindness effect);
        public void RandomizeContrast(Contrast effect);
        public void RandomizeFilterMatrix(FilterMatrix effect);
        public void RandomizeGlow(Glow effect);
        public void RandomizeGrayScale(GrayScale effect);
        public void RandomizeHistogramEqualization(HistogramEqualization effect);
        public void RandomizeHSBCorrection(HSBCorrection effect);
        public void RandomizeInvert(Invert effect);
        public void RandomizeKodaChrome(KodaChrome effect);
        public void RandomizeLightness(Lightness effect);
        public void RandomizeLomograph(Lomograph effect);
        public void RandomizeOpacity(Opacity effect);
        public void RandomizePolaroid(Polaroid effect);
        public void RandomizeSepia(Sepia effect);
        public void RandomizeVignette(Vignette effect);
        public void RandomizeBokehBlur(BokehBlur effect);
        public void RandomizeBoxBlur(BoxBlur effect);
        public void RandomizeEdgeDetection(EdgeDetection effect);
        public void RandomizeGaussianBlur(GaussianBlur effect);
        public void RandomizeGaussianSharpen(GaussianSharpen effect);
        public void RandomizeQuantize(Quantize effect);
        public void RandomizeBulge(Bulge effect);
        public void RandomizeOilPaint(OilPaint effect);
        public void RandomizePolarCoordinates(PolarCoordinates effect);
        public void RandomizeRipple(Ripple effect);
        public void RandomizeSlitScan(SlitScan effect);
        public void RandomizeSwirl(Swirl effect);
        public void RandomizeWave(Wave effect);
        public void RandomizeCrystallize(Crystallize effect);
        public void RandomizeDithering(Dithering effect);
        public void RandomizePixelate(Pixelate effect);
        public void RandomizeRGBShift(RGBShift effect);
        public void RandomizeSlices(Slices effect);
        public void RandomizeGaussianNoise(GaussianNoise effect);
        public void RandomizePerlinNoise(PerlinNoise effect);
        public void RandomizeCrop(Crop effect);
        public void RandomizeEntropyCrop(EntropyCrop effect);
        public void RandomizeFlip(Flip effect);
        public void RandomizePad(Pad effect);
        public void RandomizeResize(Resize effect);
        public void RandomizeRotate(Rotate effect);
        public void RandomizeScale(Scale effect);
        public void RandomizeShift(Shift effect);
        public void RandomizeSkew(Skew effect);
    }
}
