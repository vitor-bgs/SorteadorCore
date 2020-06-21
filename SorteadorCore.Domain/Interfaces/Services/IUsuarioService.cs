using SorteadorCore.Domain.Entities;

namespace SorteadorCore.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Usuario ObterUsuario(string username, string password);
    }
}
