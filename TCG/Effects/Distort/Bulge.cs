﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class Bulge : IEffect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public float Radius { get; set; }
        public float Strenght { get; set; }

        public Bulge(int x, int y, float radius, float strenght)
        {
            X = x;
            Y = y;
            Radius = radius;
            Strenght = strenght;
        }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Bulge(X, Y, Radius, Strenght));
    }
}