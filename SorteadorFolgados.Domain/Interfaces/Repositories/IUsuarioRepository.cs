using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario ObterUsuario(string username, string password);
    }
}
