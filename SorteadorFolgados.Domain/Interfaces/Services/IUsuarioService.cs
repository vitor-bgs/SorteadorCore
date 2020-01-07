using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Usuario ObterUsuario(string username, string password);
    }
}
