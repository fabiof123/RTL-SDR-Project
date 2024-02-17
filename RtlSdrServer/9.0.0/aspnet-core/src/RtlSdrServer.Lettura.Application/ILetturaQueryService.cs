using Abp.Dependency;
using RtlSdrServer.Lettura.Application.WiewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public interface ILetturaQueryService : ITransientDependency
    {
        Task<IEnumerable<LetturaProjection>> GetAllLetture(decimal? latitudine = null, decimal? longitudine = null, decimal? freqIniziale = null, decimal? freqFinale = null);

        Task<LetturaDettagliProjection> GetLettura(long id);

        Task<List<PosizioniProjection>> ElencoPosizioniLetture();

        Task<List<FrequenzeProjection>> GetFrequenzeDaPosizione(decimal latitudine, decimal longitudine);
    }
}
