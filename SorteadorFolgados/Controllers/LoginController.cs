using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginAppService _loginAppService;
        private readonly IMapper _mapper;

        public LoginController(
            IMapper mapper,
            ILoginAppService loginAppService)
        {
            _mapper = mapper;
            _loginAppService = loginAppService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Salas");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(UsuarioViewModel usuario)
        {
            try
            {
                if(_loginAppService.Login(_mapper.Map<UsuarioViewModel, Usuario>(usuario)))
                {
                    await Login(usuario);
                    return RedirectToAction("Index", "Salas");
                }
                else
                {
                    ViewBag.Erro = "Usuário e / ou senha incorretos!";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Erro = "Ocorreu um erro ao tentar efetuar o login";
            }
            return View("Index");
        }

        private async Task Login(UsuarioViewModel usuario)
        {
            var claims = new List<Claim>
            {
                new Claim("user", usuario.Username),
                new Claim("role", "admin")
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Cookies", "user", "role");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(1),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(claimsPrincipal);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("TestScheme");
            return RedirectToAction("Index");
        }
    }
}