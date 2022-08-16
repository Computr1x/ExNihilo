using SixLabors.ImageSharp;
using System.Reflection;

namespace ExNihilo.Base;

public abstract class Renderable
{
    public abstract void Render(Image image, GraphicsOptions graphicsOptions);

    public void RandomizeProperties(Random random, bool force = false)
    {
        PropertyInfo[] properties = GetType().GetProperties();
        PropertyInfo propertyInfo;

        for (int i = 0; i < properties.Length; i++)
        {
            propertyInfo = properties[i];

            if (propertyInfo.GetValue(this) is Property property)
                property.Randomize(random!, force);
        }
    }
}
