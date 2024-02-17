using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Dto
{
    public class PotenzaSegnaleInput
    {
        public long LetturaId { get; set; }

        public decimal Frequenza { get; set; }

        public decimal Valore { get; set; }
    }
}
