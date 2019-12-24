using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;

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
    }
}
