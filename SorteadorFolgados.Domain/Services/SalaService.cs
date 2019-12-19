using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;

namespace SorteadorFolgados.Domain.Services
{
    public class SalaService : ServiceBase<Sala>, ISalaService
    {
        private readonly ISalaRepository _salaRepository;
        public SalaService(ISalaRepository salaRepository)
            :base(salaRepository)
        {
            _salaRepository = salaRepository;
        }
    }
}
