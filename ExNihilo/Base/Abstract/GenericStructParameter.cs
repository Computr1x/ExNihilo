﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Base.Abstract;

public abstract class GenericStructParameter<T> : IRandomizableParameter where T : struct
{
    protected T? value;
    protected bool ValueIsRandomized { get; set; } = false;

    public virtual T? Value
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

    public virtual GenericStructParameter<T> WithValue(T value)
    {
        Value = value;
        return this;
    }

    public static implicit operator T(GenericStructParameter<T> genericValue) => genericValue.Value ?? genericValue.DefaultValue;
}