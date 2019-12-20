using SorteadorFolgados.Domain.Entities;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface ISorteioDetalheService : IServiceBase<SorteioDetalhe>
    {
        List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId);
    }
}
