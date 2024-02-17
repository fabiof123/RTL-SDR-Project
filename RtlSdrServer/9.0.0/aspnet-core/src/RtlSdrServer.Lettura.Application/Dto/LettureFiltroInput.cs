using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Dto
{
    public class LettureFiltroInput
    {
        public decimal? FrequenzaIniziale { get; set; } = null;

        public decimal? FrequenzaFinale { get; set; } = null;

        public decimal? Latitudine { get; set; } = null;

        public decimal? Longitudine { get; set; } = null;
    }
}
