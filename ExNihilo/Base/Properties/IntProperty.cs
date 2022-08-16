namespace ExNihilo.Base;

public class IntProperty : NumericProperty<int>
{
    public IntProperty(int defaultValue = default) : base(defaultValue) { }

    public IntProperty(int min, int max, int defaultValue = default) : base(min, max, defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.Next(Min, Max);
    }
}
