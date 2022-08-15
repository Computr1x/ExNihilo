using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExNihilo.Base.Interfaces;

public abstract class Property
{

}

public class ConstantProperty<T> : Property
{
    public ConstantProperty(T value)
    {
        Value = value;
    }

    public T Value { get; init; }
}

public abstract class RandomProperty : Property
{
    public abstract void Randomize(Random r);
}

public abstract class GenericRandomProperty<T> : RandomProperty
{
    protected GenericRandomProperty(T? defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public T? DefaultValue { get; set; }
}