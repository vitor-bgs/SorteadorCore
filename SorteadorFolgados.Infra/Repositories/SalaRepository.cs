using Microsoft.EntityFrameworkCore;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace SorteadorFolgados.Infra.Repositories
{
    public class SalaRepository : RepositoryBase<Sala, BancoContexto>, ISalaRepository
    {
        public SalaRepository(BancoContexto db) : base(db)
        {
        }
        public override void Remove(Sala obj)
        {
            obj.Ativo = false;
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
