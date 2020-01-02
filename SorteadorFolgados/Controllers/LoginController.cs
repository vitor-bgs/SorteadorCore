using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<UsuarioViewModel> _signInManager;

        public LoginController(SignInManager<UsuarioViewModel> signInManager)
        {
            _signInManager = signInManager;
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
                if(usuario.Username == "admin" && usuario.Password == "senhasegura")
                {
                    await Login(usuario);
                    return RedirectToAction("Index", "Salas");
                }
                else
                {
                    ViewBag.Erro = "Usuário e / ou senha incorretos!";
                }
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um erro ao tentar efetuar o login";
            }
            return View("Index");
        }

        private async Task Login(UsuarioViewModel usuario)
        {

            await _signInManager.PasswordSignInAsync(usuario.Username, usuario.Password, true, true);
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, usuario.Username),
            //    new Claim(ClaimTypes.Role, "admin")
            //};

            //var identidadeDeUsuario = new ClaimsIdentity(claims, "cookie");
            //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            //var propriedadesDeAutenticacao = new AuthenticationProperties
            //{
            //    AllowRefresh = true,
            //    ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(1),
            //    IsPersistent = true
            //};

            //await HttpContext.SignInAsync("TestScheme", claimsPrincipal, propriedadesDeAutenticacao);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("TestScheme");
            return RedirectToAction("Index");
        }
    }
}