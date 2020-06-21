using SorteadorCore.Application.Interfaces;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Services;

namespace SorteadorCore.Application
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
