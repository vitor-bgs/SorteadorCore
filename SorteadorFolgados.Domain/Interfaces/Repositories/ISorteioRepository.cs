using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Interfaces.Repository
{
    public interface ISorteioRepository :IRepositoryBase<Sorteio>
    {
        Sorteio ObterSorteioAtual();
        List<Sorteio> ObterSorteiosPorData(DateTime dataInicial, DateTime dataFinal);
    }
}
