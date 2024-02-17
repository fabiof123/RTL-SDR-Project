using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class PotenzaSegnale : Entity<long>
    {
        public long LetturaId { get; private set; }

        public decimal Frequenza { get; private set; }

        public decimal Valore { get; private set; }

        protected PotenzaSegnale() { }

        public static PotenzaSegnale Crea(decimal frequenza, decimal valore)
        {
            /*Aggiungere controlli per la validita dei dati!!!*/

            return new PotenzaSegnale()
            {
                Frequenza = frequenza,
                Valore = valore
            };
        }   
    }
}
