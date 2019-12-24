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
    public class SalaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISalaAppService _salaAppService;
        private readonly ISorteioAppService _sorteioAppService;

        public SalaController(IMapper mapper, ISalaAppService salaAppService, ISorteioAppService sorteioAppService)
        {
            _mapper = mapper;
            _salaAppService = salaAppService;
            _sorteioAppService = sorteioAppService;
        }

        // GET: Sala
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

        // GET: Sala/Details/5
        public ActionResult Details(int id)
        {
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(salas);
        }

        // GET: Sala/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sala/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalaViewModel sala)
        {
            try
            {
                var salaDomain = _mapper.Map<SalaViewModel, Sala>(sala);
                _salaAppService.Add(salaDomain);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        // GET: Sala/Edit/5
        public ActionResult Edit(int id)
        {
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(salas);
        }

        // POST: Sala/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SalaViewModel sala)
        {
            try
            {
                _salaAppService.Update(_mapper.Map<SalaViewModel, Sala>(sala));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Sala/Delete/5
        public ActionResult Delete(int id)
        {
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(salas);
        }

        // POST: Sala/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SalaViewModel sala)
        {
            try
            {
                sala.SalaId = id;
                _salaAppService.Remove(_mapper.Map<SalaViewModel, Sala>(sala));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult IniciarSorteio(int SalaId)
        {
            var sala = _salaAppService.Get(SalaId);
            _sorteioAppService.IniciarNovoSorteio(sala);
            return RedirectToAction(nameof(Index));
        }
    }
}