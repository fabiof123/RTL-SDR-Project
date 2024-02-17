using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using RtlSdrServer.Lettura.Application.WiewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaQueryService : ILetturaQueryService
    {
        private readonly IRepository<Lettura, long> _letturaRepository;

        public LetturaQueryService(IRepository<Lettura, long> letturaRepository)
        {
            _letturaRepository = letturaRepository;
        }

        public async Task<IEnumerable<LetturaProjection>> GetAllLetture(decimal? latitudine = null, decimal? longitudine = null, decimal? freqIniziale = null, decimal? freqFinale = null)
        {
            return await (from letture in _letturaRepository.GetAll()
                          .WhereIf(latitudine.HasValue, x => x.Posizione.Latitudine == latitudine)
                          .WhereIf(longitudine.HasValue, x => x.Posizione.Longitudine == longitudine)
                          .WhereIf(freqIniziale.HasValue, x => x.FrequenzaIniziale == freqIniziale)
                          .WhereIf(freqFinale.HasValue, x => x.FrequenzaFinale == freqFinale)
                          select new LetturaProjection()
                          {
                              Id = letture.Id,
                              Data = letture.Data,
                              FrequenzaIniziale = letture.FrequenzaIniziale,
                              FrequenzaFinale = letture.FrequenzaFinale,
                              Latitudine = letture.Posizione.Latitudine,
                              Longitudine = letture.Posizione.Longitudine,
                          }).ToListAsync();
        }

        public async Task<LetturaDettagliProjection> GetLettura(long id)
        {
            var lettura = await _letturaRepository.GetAsync(id);
            return new LetturaDettagliProjection()
            {
                Id = lettura.Id,
                Data = lettura.Data,
                FrequenzaIniziale = lettura.FrequenzaIniziale,
                FrequenzaFinale = lettura.FrequenzaFinale,
                Latitudine = lettura.Posizione.Latitudine,
                Longitudine = lettura.Posizione.Longitudine,
                PotenzaSegnali = lettura.PotenzaSegnali.Select(x => new PotenzaSegnaleProjection()
                {
                    Id = x.Id,
                    Frequenza = x.Frequenza,
                    Valore = x.Valore,
                }).ToList(),
            };
        }

        public async Task<List<PosizioniProjection>> ElencoPosizioniLetture()
        {
            return await _letturaRepository.GetAll().Select(x => new PosizioniProjection
            {
                Latitudine = x.Posizione.Latitudine,
                Longitudine = x.Posizione.Longitudine,
            }).Distinct().ToListAsync();
        }

        public async Task<List<FrequenzeProjection>> GetFrequenzeDaPosizione(decimal latitudine, decimal longitudine)
        {
            return await _letturaRepository.GetAll()
                .Where(x => x.Posizione.Latitudine == latitudine && x.Posizione.Longitudine == longitudine)
                .Select(x => new FrequenzeProjection
                {
                    FrequenzaIniziale = x.FrequenzaIniziale,
                    FrequenzaFinale = x.FrequenzaFinale,
                }).Distinct().ToListAsync();
        }
    }
}
