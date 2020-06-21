using SorteadorCore.Domain.Entities;

namespace SorteadorCore.Application.Interfaces
{
    public interface ILoginAppService
    {
        bool Login(Usuario usuario);
    }
}
