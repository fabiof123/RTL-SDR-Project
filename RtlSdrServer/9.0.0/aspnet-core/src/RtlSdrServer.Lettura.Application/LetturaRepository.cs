using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaRepository : ILetturaRepository
    {
        private readonly IRepository<Lettura, long> _letturaRepository;

        public LetturaRepository(IRepository<Lettura, long> letturaRepository)
        {
            _letturaRepository = letturaRepository;
        }


        public async Task<long> CreaLettura(Lettura lettura)
        {
            return await _letturaRepository.InsertAndGetIdAsync(lettura);
        }

        public async Task<Lettura> GetLetturaById(long id)
        {
            return await _letturaRepository.GetAsync(id);
        }

        public async Task UpdateLettura(Lettura lettura)
        {
            await _letturaRepository.UpdateAsync(lettura);
        }
    }
}
