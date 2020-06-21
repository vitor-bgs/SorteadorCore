using SorteadorCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario ObterUsuario(string username, string password);
    }
}
