using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Infra.Context;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Infra.Repositories
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
