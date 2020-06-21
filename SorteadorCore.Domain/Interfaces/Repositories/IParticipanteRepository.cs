using SorteadorCore.Domain.Entities;

namespace SorteadorCore.Domain.Interfaces.Repository
{
    public interface IParticipanteRepository : IRepositoryBase<Participante>
    {
        Participante BuscaPorNome(string nomeParticipante);
    }
}
