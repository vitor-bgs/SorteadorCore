using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SalasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISalaAppService _salaAppService;
        private readonly ISorteioAppService _sorteioAppService;

        public SalasController(IMapper mapper, ISalaAppService salaAppService, ISorteioAppService sorteioAppService)
        {
            _mapper = mapper;
            _salaAppService = salaAppService;
            _sorteioAppService = sorteioAppService;
        }

        public ActionResult Index()
        {
            var salas = _salaAppService.GetAll().Select(s => _mapper.Map<Sala, SalaViewModel>(s));
            var sorteio = _sorteioAppService.ObterSorteioAtual();
            if(sorteio == null)
            {
                return View(salas.OrderBy(s => s.SalaId));
            }
            return View(salas.Select(s => { s.EstaNoSorteioAtual = s.SalaId == sorteio.SalaId; return s; }).OrderBy(s => s.SalaId));
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(salas);
        }
        public ActionResult Delete(int id)
        {
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(salas);
        }
    }
}