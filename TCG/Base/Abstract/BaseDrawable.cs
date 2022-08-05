using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;

public abstract class BaseDrawable : IDrawable
{
    public IList<IEffect> Effects { get; } = new List<IEffect>();

    public BaseDrawable WithEffect(IEffect effect)
    {
        Effects.Add(effect);
        return this;
    }

    public abstract void Render(Image image, GraphicsOptions graphicsOptions);
}
