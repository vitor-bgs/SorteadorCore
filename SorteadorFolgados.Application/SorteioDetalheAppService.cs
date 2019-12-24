using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SorteadorFolgados.Application
{
    public class SorteioDetalheAppService : AppServiceBase<SorteioDetalhe>, ISorteioDetalheAppService
    {
        private readonly ISorteioDetalheService _sorteioDetalheService;
        private readonly ISorteioService _sorteioService;
        private readonly IParticipanteService _participanteService;
        public SorteioDetalheAppService(ISorteioDetalheService sorteioDetalheService, IParticipanteService participanteService, ISorteioService sorteioService) : base(sorteioDetalheService)
        {
            _sorteioDetalheService = sorteioDetalheService;
            _sorteioService = sorteioService;
            _participanteService = participanteService;
        }

        public List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId)
        {
            return _sorteioDetalheService.GetSorteioDetalhes(sorteioId);
        }

        public void Sortear(string nomeParticipante, string EnderecoIP)
        {
            var participante = _participanteService.BuscaPorNome(nomeParticipante);
            var sorteioAtual = _sorteioService.ObterSorteioAtual();
            _sorteioDetalheService.Sortear(sorteioAtual, participante, EnderecoIP);
        }
    }
}
