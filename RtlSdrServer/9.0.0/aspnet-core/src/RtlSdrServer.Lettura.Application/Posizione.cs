using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class Posizione : ValueObject
    {
        public decimal Latitudine { get; private set; }

        public decimal Longitudine { get; private set; }

        protected Posizione() { }

        public static Posizione Crea(decimal latitudine, decimal longitudine)
        {
            /*Controlli validità (MAX e ;IN valore ....)*/
            /*se devo accorciare il numero di cifre dopo la virgola 
             String.Format("{0:0.00}", 123.4567); */

            return new Posizione()
            {
                Latitudine = latitudine,
                Longitudine = longitudine
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Latitudine;
            yield return Longitudine;
        }
    }
}
