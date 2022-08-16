namespace ExNihilo.Base;

public class FloatProperty : NumericProperty<float>
{
    public FloatProperty(float defaultValue = default) : base(defaultValue) { }

    public FloatProperty(float min, float max, float defaultValue = default) : base(min, max, defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() * (Max - Min) + Min;
    }
}
