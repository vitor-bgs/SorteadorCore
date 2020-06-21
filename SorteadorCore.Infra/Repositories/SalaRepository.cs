using Microsoft.EntityFrameworkCore;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace SorteadorCore.Infra.Repositories
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
