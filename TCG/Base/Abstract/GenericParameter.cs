using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;

public abstract class GenericParameter<T> : IRandomizableParameter where T : class
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

    public GenericParameter(T defaultValue)
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

    public virtual GenericParameter<T> WithValue(T value)
    {
        Value = value;
        return this;
    }

    public static implicit operator T(GenericParameter<T> genericValue) => genericValue.Value ?? genericValue.DefaultValue;  
}
