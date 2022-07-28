﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Rnd.Randomizers.Parameters;

public class EnumParameter<T> : GenericStructParameter<T> where T : struct 
{
    public T[] EnumValues { get; set; }

    public EnumParameter(T defaultValue) : base(defaultValue)
    {
        EnumValues = (T[])Enum.GetValues(typeof(T));
    }

    public EnumParameter(T defaultValue, List<T> enumValues) : base(defaultValue)
    {
        EnumValues = enumValues.ToArray();
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = EnumValues[r.Next(EnumValues.Length)];
    }
}