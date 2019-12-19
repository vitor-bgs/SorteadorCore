using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Services;
using SorteadorFolgados.Infra.Repositories;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SorteioController : Controller
    {
        private readonly IMapper _mapper;

        public SorteioController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            SorteioService sorteioService = new SorteioService(new SorteioRepository());
            SorteioDetalheService sorteioDetalheService = new SorteioDetalheService(new SorteioDetalheRepository());

            var sorteio = sorteioService.ObterSorteioAtual();
            sorteio.Participacoes = sorteioDetalheService.GetAll().OrderByDescending(p => p.Pontos).ToList();
            return View(_mapper.Map<Sorteio,SorteioViewModel>(sorteio));
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
            if (sorteioService.ObterSorteioAtual().Participacoes.Any(p => p.Participante.Nome == nome))
            {
                return RedirectToAction("Index");
            }

            string enderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            new SorteioDetalheService(new SorteioDetalheRepository()).Add(new SorteioDetalhe() { Participante = new Participante() { Nome = nome } });
            return RedirectToAction("Index");
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