
using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface IParticipanteService : IServiceBase<Participante>
    {
        Participante BuscaPorNome(string nomeParticipante);
    }
}
