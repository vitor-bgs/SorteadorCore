using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;
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
            var participacao = new SorteioDetalhe() { SorteioId = _sorteioService.ObterSorteioAtual().SorteioId, EnderecoIP = enderecoIP, Participante = new Participante() { Nome = nome } };
            _sorteioDetalheService.Add(participacao);
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