﻿using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class BoolParameter : GenericStructParameter<bool>
{
    public BoolParameter(bool defaultValue = false) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() > 0.5f;
    }
}