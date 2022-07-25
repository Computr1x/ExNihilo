using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Interfaces
{
    public interface IRandomInititializer
    {
        public void RandomInitialize(IRandomEffectGenerator generator);
    }
}
