using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;

namespace SorteadorFolgados.Application
{
    public class ParticipanteAppService : AppServiceBase<Participante>, IParticipanteAppService
    {
        private readonly IParticipanteService _participanteService;

        public ParticipanteAppService(IParticipanteService participanteService)
            :base(participanteService)
        {
            _participanteService = participanteService;
        }
    }
}
