using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Infra.Context;

namespace SorteadorCore.Infra.Repositories
{
    public class SorteioDetalheRepository : RepositoryBase<SorteioDetalhe, BancoContexto>, ISorteioDetalheRepository
    {
        public SorteioDetalheRepository(BancoContexto db) : base(db)
        {
        }

        public List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId)
        {
            return Db.SorteioDetalhes.Include(s => s.Participante).Where(s => s.SorteioId == sorteioId).ToList();
        }
    }
}
