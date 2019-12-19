using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Domain.Interfaces.Repository
{
    public interface IParticipanteRepository : IRepositoryBase<Participante>
    {
        Participante BuscaPorNome(string nomeParticipante);
    }
}
