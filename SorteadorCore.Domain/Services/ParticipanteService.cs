using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Domain.Interfaces.Services;

namespace SorteadorCore.Domain.Services
{
    public class ParticipanteService : ServiceBase<Participante>, IParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;

        public ParticipanteService(IParticipanteRepository participanteRepository)
            : base(participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public Participante BuscaPorNome(string nomeParticipante)
        {
            if(_participanteRepository.BuscaPorNome(nomeParticipante) == null)
            {
                _participanteRepository.Add(new Participante() { Nome = nomeParticipante });
            }
            return _participanteRepository.BuscaPorNome(nomeParticipante);
        }
    }
}
