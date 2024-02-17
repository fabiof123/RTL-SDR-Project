using Abp.Domain.Entities;
using RtlSdrServer.Lettura.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class Lettura : AggregateRoot<long>
    {
        public DateTime Data { get; private set; }

        public decimal FrequenzaIniziale { get; private set; }

        public decimal FrequenzaFinale { get; private set; }

        public Posizione Posizione { get; private set; }

        public IReadOnlyCollection<PotenzaSegnale> PotenzaSegnali => _potenzaSegnali.AsReadOnly();

        private readonly List<PotenzaSegnale> _potenzaSegnali = new List<PotenzaSegnale>();

        protected Lettura() { }

        public static Lettura Crea(DateTime data, decimal frequenzaIniziale, decimal frequenzaFinale, decimal latitudine, decimal longitudine)
        {
            if (frequenzaFinale <= frequenzaIniziale)
            {
                throw new FrequenzaNonValidaExceptions("Range di frequenze non valido");
            }
            if (data > DateTime.Now)
            {
                throw new DataNonValidaExceptions("La data inserita non è valida");
            }

            return new Lettura()
            {
                Data = data,
                FrequenzaIniziale = frequenzaIniziale,
                FrequenzaFinale = frequenzaFinale,
                Posizione = Posizione.Crea(latitudine, longitudine)
            };
        }

        public void AggiungiPotenzaSegnale(decimal frequenza, decimal valore)
        {
            if (frequenza < FrequenzaIniziale || frequenza > FrequenzaFinale)
            {
                throw new FrequenzaNonValidaExceptions("Frequenza fuori dal range della lettura");
            }
            if ((_potenzaSegnali.Where(x => x.Frequenza == frequenza).ToList()).Any())
            {
                throw new FrequenzaNonValidaExceptions("Frequenza non univoca");
            }

            PotenzaSegnale potenzaSegnale = PotenzaSegnale.Crea(frequenza, valore);
            _potenzaSegnali.Add(potenzaSegnale);
        }
    }
}
