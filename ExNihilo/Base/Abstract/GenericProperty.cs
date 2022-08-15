using ExNihilo.Base.Interfaces;

namespace ExNihilo.Base.Abstract;

public abstract class GenericProperty<T> : IRandomizableProperty where T : class
{
    protected T value;
    protected T defaultValue;
    protected bool ValueIsRandomized { get; set; } = false;

    public T Value { 
        get => value; 
        set
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
            ValueIsRandomized = false;
        }
    }

    public virtual T DefaultValue { get => defaultValue; set => defaultValue = value; }

    public GenericProperty(T defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public void Randomize(Random r, bool force = false)
    {
        if (Value != null && !ValueIsRandomized && !force)
            return;
        RandomizeImplementation(r); 
        ValueIsRandomized = true; 
    }
    protected abstract void RandomizeImplementation(Random r);

    public virtual GenericProperty<T> WithValue(T value)
    {
        Value = value;
        return this;
    }

    public static implicit operator T(GenericProperty<T> genericValue) => genericValue.Value ?? genericValue.DefaultValue;  
}
