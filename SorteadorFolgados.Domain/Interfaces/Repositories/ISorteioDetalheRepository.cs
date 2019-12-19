using SorteadorFolgados.Domain.Entities;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Interfaces.Repository
{
    public interface ISorteioDetalheRepository : IRepositoryBase<SorteioDetalhe>
    {
        List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId);
    }
}
