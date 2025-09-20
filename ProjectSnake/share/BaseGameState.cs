using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share
{
    internal abstract class BaseGameState
    {
        public abstract void Update(float deltaTIme);
        public abstract void Reset();
    }
}
