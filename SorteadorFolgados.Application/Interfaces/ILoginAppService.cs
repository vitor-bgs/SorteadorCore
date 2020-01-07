using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Application.Interfaces
{
    public interface ILoginAppService
    {
        bool Login(Usuario usuario);
    }
}
