using System.Collections.Generic;
using System.Linq;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;

namespace SorteadorFolgados.Infra.Repositories
{
    public class SorteioDetalheRepository : RepositoryBase<SorteioDetalhe>, ISorteioDetalheRepository
    {
        public List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId)
        {
            return BancoDadosFake<SorteioDetalhe>.Lista.Where(s => s.SorteioId == sorteioId).ToList();
        }
    }
}
