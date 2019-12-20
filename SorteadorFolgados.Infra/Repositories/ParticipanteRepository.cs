using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Infra.Repositories
{
    public class ParticipanteRepository : RepositoryBase<Participante>, IParticipanteRepository
    {
        public Participante BuscaPorNome(string nomeParticipante)
        {
            return BancoDadosFake<Participante>.Lista.FirstOrDefault(p => p.Nome == nomeParticipante);
        }

        public override Participante Add(Participante entity)
        {
            entity.ParticipanteId = BancoDadosFake<Participante>.Lista.Count + 1;
            return base.Add(entity);
        }

        public override Participante Get(int entity)
        {
            return BancoDadosFake<Participante>.Lista.FirstOrDefault(p => p.ParticipanteId == entity);
        }
    }
}
