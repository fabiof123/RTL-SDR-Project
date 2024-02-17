using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Dto
{
    public class LetturaInput
    {
        public DateTime? Data { get; set; } = null;

        [Required]
        public decimal FrequenzaIniziale { get; set; }

        [Required]
        public decimal FrequenzaFinale { get; set; }

        [Required]
        public decimal Latitudine { get; set; }

        [Required]
        public decimal Longitudine { get; set; }

        public List<PSegnaleInput> PotenzeSegnali { get; set; } = new List<PSegnaleInput>();


        public class PSegnaleInput
        {
            [Required]
            public decimal Frequenza { get; set; }

            [Required]
            public decimal Valore { get; set; }
        }
    }
}
