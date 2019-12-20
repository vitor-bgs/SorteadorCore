using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System.Linq;

namespace SorteadorFolgados.Infra.Repositories
{
    public class SalaRepository : RepositoryBase<Sala>, ISalaRepository
    {
        public override Sala Add(Sala entity)
        {
            entity.SalaId = BancoDadosFake<Sala>.Lista.Count + 1;
            return base.Add(entity);
        }

        public override Sala Get(int entity)
        {
            return BancoDadosFake<Sala>.Lista.FirstOrDefault(p => p.SalaId == entity);
        }

        public override void Update(Sala entity)
        {
            var sala = BancoDadosFake<Sala>.Lista.First(p => p.SalaId == entity.SalaId);
            BancoDadosFake<Sala>.Lista.Remove(sala);
            BancoDadosFake<Sala>.Lista.Add(entity);
        }

        public override void Remove(Sala entity)
        {
            var sala = BancoDadosFake<Sala>.Lista.First(p => p.SalaId == entity.SalaId);
            BancoDadosFake<Sala>.Lista.Remove(sala);
        }
    }
}
