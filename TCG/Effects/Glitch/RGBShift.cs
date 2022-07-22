using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class RGBShift : IEffect
    {
        int redXOffset, greenXOffset, blueXOffset, redYOffset, greenYOffset, blueYOffset;

        public int BlueYOffset { get => blueYOffset; set => blueYOffset = value; }
        public int GreenYOffset { get => greenYOffset; set => greenYOffset = value; }
        public int RedYOffset { get => redYOffset; set => redYOffset = value; }
        public int BlueXOffset { get => blueXOffset; set => blueXOffset = value; }
        public int GreenXOffset { get => greenXOffset; set => greenXOffset = value; }
        public int RedXOffset { get => redXOffset; set => redXOffset = value; }

        public RGBShift(int redXOffset, int greenXOffset, int blueXOffset, int redYOffset, int greenYOffset, int blueYOffset)
        {
            RedXOffset = redXOffset;
            GreenXOffset = greenXOffset;
            BlueXOffset = blueXOffset;
            RedYOffset = redYOffset;
            GreenYOffset = greenYOffset;
            BlueYOffset = blueYOffset;
        }

        public RGBShift(int offset)
        {
            RedXOffset = RedYOffset = offset;
            GreenXOffset = GreenYOffset = -offset;
            BlueXOffset = offset;
            BlueYOffset = -offset;
        }

        public void Render(Image image, GraphicsOptions graphicsOptions) => 
            image.Mutate(x => 
                x.RGBShift(RedXOffset, GreenXOffset, BlueXOffset, RedYOffset, GreenYOffset, BlueYOffset));
        
    }
}
