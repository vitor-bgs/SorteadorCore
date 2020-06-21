using SorteadorCore.Domain.Entities;
using System.Collections.Generic;

namespace SorteadorCore.Domain.Interfaces.Repository
{
    public interface ISorteioDetalheRepository : IRepositoryBase<SorteioDetalhe>
    {
        List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId);
    }
}
