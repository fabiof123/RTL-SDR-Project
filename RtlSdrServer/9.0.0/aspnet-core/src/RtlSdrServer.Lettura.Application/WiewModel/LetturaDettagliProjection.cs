using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.WiewModel
{
    public class LetturaDettagliProjection
    {
        public long Id { get; init; }

        public DateTime Data { get; init; }

        public decimal FrequenzaIniziale { get; init; }

        public decimal FrequenzaFinale { get; init; }

        public decimal Latitudine { get; init; }

        public decimal Longitudine { get; init; }

        public List<PotenzaSegnaleProjection> PotenzaSegnali { get; init; }
    }
}
