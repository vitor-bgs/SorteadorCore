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


        public override SorteioDetalhe Add(SorteioDetalhe entity)
        {
            entity.SorteioDetalheId = BancoDadosFake<SorteioDetalhe>.Lista.Count + 1;
            return base.Add(entity);
        }

        public override SorteioDetalhe Get(int entity)
        {
            return BancoDadosFake<SorteioDetalhe>.Lista.FirstOrDefault(p => p.SorteioDetalheId == entity);
        }
    }
}
