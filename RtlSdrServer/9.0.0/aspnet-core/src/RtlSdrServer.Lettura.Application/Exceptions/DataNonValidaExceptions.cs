using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Exceptions
{
    [Serializable]
    public class DataNonValidaExceptions : Exception
    {
        public DataNonValidaExceptions() { }

        public DataNonValidaExceptions(string message) : base(message) { }

        public DataNonValidaExceptions(string message, Exception inner) : base(message, inner) { }
    }
}
