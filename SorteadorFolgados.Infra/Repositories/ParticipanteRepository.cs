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
    }
}
