using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SorteioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISorteioDetalheAppService _sorteioDetalheAppService;

        public SorteioController(IMapper mapper, ISorteioAppService sorteioAppService, ISorteioDetalheAppService sorteioDetalheAppService)
        {
            _mapper = mapper;
            _sorteioAppService = sorteioAppService;
            _sorteioDetalheAppService = sorteioDetalheAppService;
        }

        public IActionResult Index()
        {

            var sorteioAtual = _sorteioAppService.ObterSorteioAtual();
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
            string enderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _sorteioDetalheAppService.Sortear(nome, enderecoIP);
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