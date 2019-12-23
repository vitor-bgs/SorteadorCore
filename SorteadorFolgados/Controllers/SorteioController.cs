using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;
using SorteadorFolgados.Domain.Services;
using SorteadorFolgados.Infra.Repositories;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SorteioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISorteioService _sorteioService;
        private readonly ISorteioDetalheService _sorteioDetalheService;

        public SorteioController(IMapper mapper, ISorteioService sorteioService, ISorteioDetalheService sorteioDetalheService)
        {
            _mapper = mapper;
            _sorteioService = sorteioService;
            _sorteioDetalheService = sorteioDetalheService;
        }

        public IActionResult Index()
        {

            var sorteioAtual = _sorteioService.ObterSorteioAtual();
            sorteioAtual.Participacoes = _sorteioDetalheService.GetSorteioDetalhes(sorteioAtual.SorteioId).OrderByDescending(p => p.Pontos).ToList();
            return View(_mapper.Map<Sorteio,SorteioViewModel>(sorteioAtual));
        }

        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sortear(string nome)
        {
            if (_sorteioService.ObterSorteioAtual().Participacoes.Any(p => p.Participante.Nome == nome))
            {
                return RedirectToAction("Index");
            }

            string enderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            new SorteioDetalheService(new SorteioDetalheRepository()).Add(new SorteioDetalhe() { SorteioId = _sorteioService.ObterSorteioAtual().SorteioId, EnderecoIP = enderecoIP, Participante = new Participante() { Nome = nome } });
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