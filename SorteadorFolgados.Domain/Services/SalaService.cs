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

        public override void Update(Sala obj)
        {
            obj.Ativo = true;
            base.Update(obj);
        }

        public override Sala Add(Sala obj)
        {
            obj.Ativo = true;
            return base.Add(obj);
        }
    }
}
