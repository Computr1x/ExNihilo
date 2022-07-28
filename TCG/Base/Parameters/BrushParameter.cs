using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Base.Parameters;

public class BrushParameter : GenericParameter<IBrush>
{
    public EnumParameter<BrushType> Type { get; set; } = new EnumParameter<BrushType>(BrushType.Solid);
    public ColorParameter Color { get; set; } = new ColorParameter(SixLabors.ImageSharp.Color.Black);

    public BrushParameter(IBrush defaultValue) : base(defaultValue)
    {
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
