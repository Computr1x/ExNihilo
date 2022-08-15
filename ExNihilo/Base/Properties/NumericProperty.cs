namespace ExNihilo.Base;

public abstract class NumericProperty<T> : GenericStructProperty<T>, IHasMinMax<T> where T : struct, IComparable<T>, IComparable
{
    protected T? minLimit, maxLimit;
    private T min, max;

    public NumericProperty(T defaultValue = default) : base(defaultValue)
    {

    }

    public NumericProperty(T minLimit, T maxLimit, T defaultValue = default) : base(defaultValue)
    {
        this.minLimit = minLimit;
        this.maxLimit = maxLimit;
    }

    public T Max { 
        get => max; 
        set 
        {
            CheckLimit(value);
            max = value; 
        } 
    }

    public T Min
    {
        get => min;
        set
        {
            CheckLimit(value);
            min = value;
        }
    }

    public override T? Value
    {
        get => value;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            CheckLimit(value.Value);
            this.value = value;
            ValueIsRandomized = false;
        }
    }

    private void CheckLimit(T value)
    {
        if (minLimit.HasValue && value.CompareTo(minLimit.Value) < 0)
            throw new ArgumentNullException($"{nameof(value)} should be equal or bigger then {minLimit}");

        if (maxLimit.HasValue && value.CompareTo(maxLimit.Value) > 0)
            throw new ArgumentNullException($"{nameof(value)} should be equal or lower then {maxLimit}");
    }

    public NumericProperty<T> WithRandomizedValue(T min, T max)
    {
        Min = min;
        Max = max;

        return this;
    }
}
