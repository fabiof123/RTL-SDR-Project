using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Dto
{
    public class LetturaVuotaInput
    {
        public DateTime Data { get; set; }

        public decimal FrequenzaIniziale { get; set; }

        public decimal FrequenzaFinale { get; set; }

        public decimal Latitudine { get; set; }

        public decimal Longitudine { get; set; }
    }
}
