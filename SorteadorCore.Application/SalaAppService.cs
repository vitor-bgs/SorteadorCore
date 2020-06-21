using SorteadorCore.Application.Interfaces;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Services;

namespace SorteadorCore.Application
{
    public class SalaAppService : AppServiceBase<Sala>, ISalaAppService
    {
        public SalaAppService(ISalaService salaService) : base(salaService)
        {
        }
    }
}
