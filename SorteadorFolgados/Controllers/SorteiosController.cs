using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.ViewModel;
using System.Collections.Generic;

namespace SorteadorFolgados.Controllers
{
    public class SorteiosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISorteioDetalheAppService _sorteioDetalheAppService;
        private readonly ISalaAppService _salaAppService;

        public SorteiosController(IMapper mapper, ISorteioAppService sorteioAppService, ISorteioDetalheAppService sorteioDetalheAppService, ISalaAppService salaAppService)
        {
            _mapper = mapper;
            _sorteioAppService = sorteioAppService;
            _sorteioDetalheAppService = sorteioDetalheAppService;
            _salaAppService = salaAppService;
        }

        public IActionResult Index()
        {
            var sorteioAtual = _mapper.Map<Sorteio,SorteioViewModel>(_sorteioAppService.ObterSorteioAtual());
            return View();
        }
    }
}