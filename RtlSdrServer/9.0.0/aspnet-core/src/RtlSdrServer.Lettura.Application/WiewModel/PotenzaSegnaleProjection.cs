using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.WiewModel
{
    public class PotenzaSegnaleProjection
    {
        public long Id { get; init; }

        public decimal Frequenza { get; init; }

        public decimal Valore { get; init; }
    }
}
