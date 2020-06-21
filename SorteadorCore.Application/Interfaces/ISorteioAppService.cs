using SorteadorCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Application.Interfaces
{
    public interface ISorteioAppService : IAppServiceBase<Sorteio>
    {
        Sorteio ObterSorteioAtual();
        void IniciarNovoSorteio(Sala sala);
        void EncerrarSorteioAtual();
        List<Sorteio> ObterSorteiosComParticipacoesVencedoras(DateTime dataInicial, DateTime dataFinal);
    }
}
