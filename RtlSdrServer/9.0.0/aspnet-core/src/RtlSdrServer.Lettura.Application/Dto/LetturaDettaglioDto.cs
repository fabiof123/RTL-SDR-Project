using RtlSdrServer.Lettura.Application.WiewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Dto
{
    public class LetturaDettaglioDto
    {
        public long Id { get; set; }

        public DateTime Data { get; set; }

        public decimal FrequenzaIniziale { get; set; }

        public decimal FrequenzaFinale { get; set; }

        public decimal Latitudine { get; set; }

        public decimal Longitudine { get; set; }

        public List<PotenzaSegnaleDto> PotenzaSegnali { get; init; }

        public class PotenzaSegnaleDto()
        {
            public long Id { get; set; }

            public decimal Frequenza { get; set; }

            public decimal Valore { get; set; }
        }
    }
}
