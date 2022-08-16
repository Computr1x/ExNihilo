namespace ExNihilo.Base;

public class EnumProperty<T> : GenericStructProperty<T> where T : struct 
{
    public T[] EnumValues { get; set; }

    public EnumProperty(T defaultValue = default) : base(defaultValue)
    {
        EnumValues = (T[]) Enum.GetValues(typeof(T));
    }

    public EnumProperty(List<T> enumValues, T defaultValue = default) : base(defaultValue)
    {
        EnumValues = enumValues.ToArray();
    }

    public EnumProperty<T> WithRandomizedValue(IEnumerable<T> values)
    {
        EnumValues = values.ToArray();
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = EnumValues[r.Next(EnumValues.Length)];
    }
}