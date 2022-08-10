using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Interfaces;

public interface ICaptcha
{
    public int Index { get; set; }
    public string Text { get; set; }
}
