﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExNihilo.Base.Interfaces;

public interface IRandomizableParameter
{
    public void Randomize(Random r, bool force = false);
}