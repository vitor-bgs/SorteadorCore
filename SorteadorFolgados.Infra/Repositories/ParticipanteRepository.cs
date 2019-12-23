using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Infra.Repositories
{
    public class ParticipanteRepository : RepositoryBase<Participante, BancoContexto>, IParticipanteRepository
    {
        public ParticipanteRepository(BancoContexto db) : base(db)
        {
        }

        public Participante BuscaPorNome(string nomeParticipante)
        {
            return Db.Participantes.FirstOrDefault(p => p.Nome == nomeParticipante);
        }
    }
}
