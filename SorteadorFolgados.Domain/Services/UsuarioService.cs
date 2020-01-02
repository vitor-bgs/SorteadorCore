using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository repository): base(repository)
        {
            _usuarioRepository = repository;
        }

        public Usuario ObterUsuario(string username, string password)
        {
            return _usuarioRepository.ObterUsuario(username, password);
        }
    }
}
