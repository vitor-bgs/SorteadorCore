using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;

namespace SorteadorFolgados.Domain.Services
{
    public class SorteioService : ServiceBase<Sorteio>, ISorteioService
    {
        ISorteioRepository _sorteioRepository;
        public SorteioService(ISorteioRepository sorteioRepository)
            :base(sorteioRepository)
        {
            _sorteioRepository = sorteioRepository;
        }

        public Sorteio ObterSorteioAtual()
        {
            return _sorteioRepository.ObterSorteioAtual();
        }

    }
}
