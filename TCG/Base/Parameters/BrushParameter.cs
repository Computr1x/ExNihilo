using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;
using SixLabors.ImageSharp;

namespace TCG.Base.Parameters;

public class BrushParameter : ComplexParameter
{
    public EnumParameter<BrushType> Type { get; set; } = new EnumParameter<BrushType>(BrushType.Solid);
    public ColorParameter Color { get; set; } = new ColorParameter(SixLabors.ImageSharp.Color.Black);

    public BrushParameter WithValue(BrushType type, Color color)
    {
        Type.WithValue(type);
        Color.WithValue(color);
        return this;
    }

    public BrushParameter WithType(BrushType type)
    {
        Type.Value = type;
        return this;
    }

    public BrushParameter WithRandomizedType()
    {
        Type.EnumValues = (BrushType[])Enum.GetValues(typeof(BrushType));
        return this;
    }

    public BrushParameter WithRandomizedType(IEnumerable<BrushType> types)
    {
        Type.EnumValues = types.ToArray();
        return this;
    }

    public BrushParameter WithColor(SixLabors.ImageSharp.Color color)
    {
        Color.Value = color;
        return this;
    }

    public BrushParameter WithRandomizedColor(int colorsCount, byte opacity = 255)
    {
        Color.Opacity = opacity;
        Color.Colors = Color.GeneratePalette(colorsCount);
        return this;
    }

    public BrushParameter WithRandomizedColor(SixLabors.ImageSharp.Color[] palette)
    {
        Color.Colors = palette;
        return this;
    }

    private IBrush GetBrush()
    {
       return Type.Value switch
        {
            BrushType.Vertical => Brushes.Vertical(Color),
            BrushType.Horizontal => Brushes.Horizontal(Color),
            BrushType.BackwardDiagonal => Brushes.BackwardDiagonal(Color),
            BrushType.ForwardDiagonal => Brushes.ForwardDiagonal(Color),
            BrushType.Min => Brushes.Min(Color),
            BrushType.Percent10 => Brushes.Percent10(Color),
            BrushType.Percent20 => Brushes.Percent20(Color),
            _ => Brushes.Solid(Color)
        };
    }

    protected override void RandomizeImplementation(Random r)
    {
        Type.Randomize(r);
        Color.Randomize(r);
    }

    public IBrush Value { get => GetValue(); }

    public IBrush GetValue()
    {
        return Type.Value switch
        {
            BrushType.Vertical => Brushes.Vertical(Color),
            BrushType.Horizontal => Brushes.Horizontal(Color),
            BrushType.BackwardDiagonal => Brushes.BackwardDiagonal(Color),
            BrushType.ForwardDiagonal => Brushes.ForwardDiagonal(Color),
            BrushType.Min => Brushes.Min(Color),
            BrushType.Percent10 => Brushes.Percent10(Color),
            BrushType.Percent20 => Brushes.Percent20(Color),
            _ => Brushes.Solid(Color)
        };
    }

    //public static implicit operator IBrush(BrushParameter brushParameter)
    //{
    //    return brushParameter.Type.Value switch
    //    {
    //        BrushType.Vertical => Brushes.Vertical(brushParameter.Color),
    //        BrushType.Horizontal => Brushes.Horizontal(brushParameter.Color),
    //        BrushType.BackwardDiagonal => Brushes.BackwardDiagonal(brushParameter.Color),
    //        BrushType.ForwardDiagonal => Brushes.ForwardDiagonal(brushParameter.Color),
    //        BrushType.Min => Brushes.Min(brushParameter.Color),
    //        BrushType.Percent10 => Brushes.Percent10(brushParameter.Color),
    //        BrushType.Percent20 => Brushes.Percent20(brushParameter.Color),
    //        _ => Brushes.Solid(brushParameter.Color)
    //    };
    //}
}
