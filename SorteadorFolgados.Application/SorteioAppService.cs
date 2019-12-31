using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace SorteadorFolgados.Application
{
    public class SorteioAppService : AppServiceBase<Sorteio>, ISorteioAppService
    {
        private readonly ISorteioService _sorteioService;
        public SorteioAppService(ISorteioService sorteioService) : base(sorteioService)
        {
            _sorteioService = sorteioService;
        }

        public void EncerrarSorteioAtual()
        {
            _sorteioService.EncerrarSorteioAtual();
        }

        public void IniciarNovoSorteio(Sala sala)
        {
            _sorteioService.IniciarNovoSorteio(sala);
        }

        public Sorteio ObterSorteioAtual()
        {
            return _sorteioService.ObterSorteioAtual();
        }

        public List<Sorteio> ObterSorteiosComParticipacoesVencedoras(DateTime dataInicial, DateTime dataFinal)
        {
            return _sorteioService.ObterVencedores(dataInicial, dataFinal);
        }
    }
}
