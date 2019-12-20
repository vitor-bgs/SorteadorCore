using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Services
{
    public class SorteioDetalheService : ServiceBase<SorteioDetalhe>, ISorteioDetalheService
    {
        private readonly ISorteioDetalheRepository _sorteioDetalhesRepository;

        public SorteioDetalheService(ISorteioDetalheRepository sorteioDetalhesRepository)
            : base(sorteioDetalhesRepository)
        {
            _sorteioDetalhesRepository = sorteioDetalhesRepository;
        }

        public override SorteioDetalhe Add(SorteioDetalhe sorteioDetalhe)
        {
            sorteioDetalhe.Pontos = new Random().Next(0, 1000);
            sorteioDetalhe.DataParticipacao = DateTime.Now;
            return base.Add(sorteioDetalhe);
        }

        public List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId)
        {
            return _sorteioDetalhesRepository.GetSorteioDetalhes(sorteioId);
        }
    }
}
