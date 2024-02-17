using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Exceptions
{
    [Serializable]
    public class FrequenzaNonValidaExceptions : Exception
    {
        public FrequenzaNonValidaExceptions(){}

        public FrequenzaNonValidaExceptions(string message) : base(message) { }

        public FrequenzaNonValidaExceptions(string message, Exception inner) : base(message, inner) { }
    }
}
