using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Application
{
    public class LoginAppService : ILoginAppService
    {
        private readonly ILoginService _loginService;
        public LoginAppService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public bool Login(Usuario usuario)
        {
            return _loginService.Login(usuario);
        }
    }
}
