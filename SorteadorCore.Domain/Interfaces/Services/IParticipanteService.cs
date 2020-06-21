
using SorteadorCore.Domain.Entities;

namespace SorteadorCore.Domain.Interfaces.Services
{
    public interface IParticipanteService : IServiceBase<Participante>
    {
        Participante BuscaPorNome(string nomeParticipante);
    }
}
