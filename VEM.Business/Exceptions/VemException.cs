using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEM.Business.Exceptions
{
    public abstract class VemException : Exception
    {
        public abstract int ExceptionCode { get; }
    }
}
