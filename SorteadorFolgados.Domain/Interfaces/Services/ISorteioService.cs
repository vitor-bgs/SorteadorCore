using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface ISorteioService : IServiceBase<Sorteio>
    {
        Sorteio ObterSorteioAtual();
        List<Sorteio> ObterVencedores(DateTime dataInicial, DateTime dataFinal);
        void IniciarNovoSorteio(Sala sala);
        void EncerrarSorteioAtual();
    }
}
