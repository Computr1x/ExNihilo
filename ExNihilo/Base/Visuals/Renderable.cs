using SixLabors.ImageSharp;
using System.Reflection;

namespace ExNihilo.Base;

public abstract class Renderable
{
    public abstract void Render(Image image, GraphicsOptions graphicsOptions);

    public void RandomizeProperties(Random random, bool force = false)
    {
        PropertyInfo[] properties = GetType().GetProperties().Where(x => x.PropertyType == typeof(Property)).ToArray();
        PropertyInfo propertyInfo;

        for (int i = 0; i < properties.Length; i++)
        {
            (properties[i].GetValue(this) as Property).Randomize(random!, force);
        }
    }
}
