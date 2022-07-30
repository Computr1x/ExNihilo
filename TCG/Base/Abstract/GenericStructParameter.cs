using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;

public abstract class GenericStructParameter<T> : IRandomizableParameter where T : struct
{
    protected T? value;
    protected bool ValueIsRandomized { get; set; } = false;

    public T? Value
    {
        get => value;
        set
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
            ValueIsRandomized = false;
        }
    }
    public T DefaultValue { get; set; }

    public GenericStructParameter(T defaultValue)
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

    public static implicit operator T(GenericStructParameter<T> genericValue) => genericValue.Value ?? genericValue.DefaultValue;
}