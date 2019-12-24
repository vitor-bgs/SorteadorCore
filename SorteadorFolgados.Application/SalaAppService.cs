using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;

namespace SorteadorFolgados.Application
{
    public class SalaAppService : AppServiceBase<Sala>, ISalaAppService
    {
        public SalaAppService(ISalaService salaService) : base(salaService)
        {
        }
    }
}
