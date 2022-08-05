using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;

namespace TCG.Base.Parameters;

public class BrushParameter : GenericParameter<IBrush>
{
    public EnumParameter<BrushType> Type { get; set; } = new EnumParameter<BrushType>(BrushType.Solid);
    public ColorParameter Color { get; set; } = new ColorParameter(SixLabors.ImageSharp.Color.Black);

    public BrushParameter() : base(Brushes.Solid(SixLabors.ImageSharp.Color.Black))
    {
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

    protected override void RandomizeImplementation(Random r)
    {
        Type.Randomize(r);
        Color.Randomize(r);

        
        Value = Type.Value switch
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

}
