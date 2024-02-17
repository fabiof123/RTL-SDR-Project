using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application.Dto
{
    public class PosizioneInput
    {
        [Required]
        public decimal Latitudine { get; set; }

        [Required]
        public decimal Longitudine { get; set; }
    }
}
