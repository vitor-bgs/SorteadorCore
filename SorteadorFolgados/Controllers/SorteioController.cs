using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Services;
using SorteadorFolgados.Infra;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SorteioController : Controller
    {
        public IActionResult Index()
        {
            SorteioService sorteioService = new SorteioService(new SorteioRepository());
            return View(sorteioService.ObterSorteioAtual());
        }

        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sortear(string nome)
        {
            SorteioService sorteioService = new SorteioService(new SorteioRepository());

            if(sorteioService.ObterSorteioAtual().Participantes.Any(p => p.Nome == nome))
            {
                return RedirectToAction("Index");
            }

            string enderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            sorteioService.SortearParticipante(nome, enderecoIP, sorteioService.ObterSorteioAtual().SorteioId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resetar(string nome, string senha)
        {
            if(senha != "resetarSorteioFolgados")
            {
                return RedirectToPage("Error");
            }
            SorteioService sorteioService = new SorteioService(new SorteioRepository());
            sorteioService.AdicionarSorteio(nome);
            return RedirectToAction("Reset");
        }

        public IActionResult OnGetPartial()
        {
            return new PartialViewResult
            {
                ViewName = "_Sortear",
                ViewData = ViewData
            };
        }
    }
}