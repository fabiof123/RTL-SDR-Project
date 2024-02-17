using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public interface ILetturaRepository : ITransientDependency
    {
        Task<long> CreaLettura(Lettura lettura);

        Task UpdateLettura(Lettura lettura);

        Task<Lettura> GetLetturaById(long id);
    }
}
