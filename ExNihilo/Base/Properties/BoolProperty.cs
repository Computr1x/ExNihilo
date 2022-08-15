using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Properties;

public class BoolProperty : GenericStructProperty<bool>
{
    public BoolProperty(bool defaultValue = false) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() > 0.5f;
    }
}