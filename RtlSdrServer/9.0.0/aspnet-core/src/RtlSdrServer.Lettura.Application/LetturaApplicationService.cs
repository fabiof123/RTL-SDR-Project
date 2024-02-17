using Abp.Application.Services;
using RtlSdrServer.Lettura.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaApplicationService : ApplicationService
    {
        private ILetturaRepository _letturaRepository;
        private ILetturaQueryService _letturaQueryService;

        public LetturaApplicationService(ILetturaRepository letturaRepository, ILetturaQueryService letturaQueryService)
        {
            _letturaRepository = letturaRepository;
            _letturaQueryService = letturaQueryService;
        }

        public async Task<long> CreateLetturaVuota(LetturaVuotaInput input)
        {
            var lettura = Lettura.Crea(input.Data, input.FrequenzaIniziale, input.FrequenzaFinale, input.Latitudine, input.Longitudine);
            var id = await _letturaRepository.CreaLettura(lettura);
            return id;
        }

        public async Task AggiungiPotanzaSegnale(PotenzaSegnaleInput input)
        {
            var lettura = await _letturaRepository.GetLetturaById(input.LetturaId);
            lettura.AggiungiPotenzaSegnale(input.Frequenza, input.Valore);
            await _letturaRepository.UpdateLettura(lettura);
        }

        public async Task<long> CreateLettura(LetturaInput input)
        {
            DateTime data = input.Data.HasValue ? input.Data.Value : DateTime.Now;
            var lettura = Lettura.Crea(data, input.FrequenzaIniziale, input.FrequenzaFinale, input.Latitudine, input.Longitudine);
            if (input.PotenzeSegnali.Any())
            {
                foreach (var potenzasegnale in input.PotenzeSegnali)
                {
                    lettura.AggiungiPotenzaSegnale(potenzasegnale.Frequenza, potenzasegnale.Valore);
                }
            }

            return await _letturaRepository.CreaLettura(lettura);
        }

        public async Task<List<LetturaDto>> GetListaLetture(LettureFiltroInput input)
        {
            return (await _letturaQueryService.GetAllLetture(input.Latitudine, input.Longitudine, input.FrequenzaIniziale, input.FrequenzaFinale)).Select(x => new LetturaDto()
            {
                Id = x.Id,
                Data = x.Data,
                FrequenzaIniziale = x.FrequenzaIniziale,
                FrequenzaFinale = x.FrequenzaFinale,
                Latitudine = x.Latitudine,
                Longitudine = x.Longitudine,
            }).OrderByDescending(x => x.Data).ToList();
        }

        public async Task<LetturaDettaglioDto> GetLettura(long id)
        {
            var lettura = await _letturaQueryService.GetLettura(id);
            return new LetturaDettaglioDto()
            {
                Id = lettura.Id,
                Data = lettura.Data,
                FrequenzaIniziale = lettura.FrequenzaIniziale,
                FrequenzaFinale = lettura.FrequenzaFinale,
                Latitudine = lettura.Latitudine,
                Longitudine = lettura.Longitudine,
                PotenzaSegnali = lettura.PotenzaSegnali.Select(x => new LetturaDettaglioDto.PotenzaSegnaleDto()
                {
                    Id = x.Id,
                    Frequenza = x.Frequenza,
                    Valore = x.Valore,
                }).ToList(),
            };
        }

        [HttpGet]
        public async Task<List<PosizioniDto>> GetListaPosizioni()
        {
            return (await _letturaQueryService.ElencoPosizioniLetture()).Select(x => new PosizioniDto()
            {
                Latitudine = x.Latitudine,
                Longitudine = x.Longitudine,
            }).ToList();
        }

        public async Task<List<FrequenzeDto>> GetListaFrequenzeNellaPosizione(PosizioneInput input)
        {
            return (await _letturaQueryService.GetFrequenzeDaPosizione(input.Latitudine, input.Longitudine)).Select(x => new FrequenzeDto()
            {
                FrequenzaIniziale = x.FrequenzaIniziale,
                FrequenzaFinale = x.FrequenzaFinale,
            }).ToList();
        }

    }
}
