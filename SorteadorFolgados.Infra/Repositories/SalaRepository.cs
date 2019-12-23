using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System.Linq;

namespace SorteadorFolgados.Infra.Repositories
{
    public class SalaRepository : RepositoryBase<Sala, BancoContexto>, ISalaRepository
    {
        public SalaRepository(BancoContexto db) : base(db)
        {
        }
    }
}
