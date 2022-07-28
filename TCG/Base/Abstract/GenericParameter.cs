using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;

public abstract class GenericParameter<T> : IRandomizableParameter where T : class
{
    protected T value;
    protected bool ValueIsRandomized { get; set; } = false;

    public T Value { 
        get => value; 
        set
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
            ValueIsRandomized = false;
        }
    }
    
    public T DefaultValue { get; set; }

    public GenericParameter(T defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public void Randomize(Random r) 
    {
        if (Value != null && !ValueIsRandomized)
            return;
        RandomizeImplementation(r); 
        ValueIsRandomized = true; 
    }
    protected abstract void RandomizeImplementation(Random r);

    public static implicit operator T(GenericParameter<T> genericValue) => genericValue.Value ?? genericValue.DefaultValue;
            
}
