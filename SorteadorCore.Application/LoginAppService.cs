using SorteadorCore.Application.Interfaces;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Application
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
