using SixLabors.ImageSharp;
using System.Reflection;

namespace ExNihilo.Base;

public abstract class Renderable
{
    public abstract void Render(Image image, GraphicsOptions graphicsOptions);

    public void RandomizeProperties(Random random, bool force = false)
    {
        PropertyInfo[] properties = GetType().GetProperties().Where(x => x.PropertyType.IsSubclassOf(typeof(Property))).ToArray();

        foreach (PropertyInfo prop in properties)
        {
            var propertyValue = prop.GetValue(this) as Property;
            propertyValue!.Randomize(random!, force);
        }
    }
}
