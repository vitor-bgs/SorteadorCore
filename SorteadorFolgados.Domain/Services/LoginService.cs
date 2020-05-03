using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioService _usuarioService;
        public LoginService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public bool Login(Usuario usuario)
        {
            return !(_usuarioService.ObterUsuario(usuario.Username, usuario.Password) == null);
        }
    }
}
