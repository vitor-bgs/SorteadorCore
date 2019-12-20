using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Services;
using SorteadorFolgados.Domain.Services;
using SorteadorFolgados.Infra.Repositories;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SalaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISalaService _salaService;
        private readonly ISorteioService _sorteioService;

        public SalaController(IMapper mapper)
        {
            _mapper = mapper;
            _salaService = new SalaService(new SalaRepository());
            _sorteioService = new SorteioService(new SorteioRepository());
        }

        // GET: Sala
        public ActionResult Index()
        {
            var salas = _salaService.GetAll().Select(s => _mapper.Map<Sala, SalaViewModel>(s));
            var sorteio = _sorteioService.ObterSorteioAtual();
            return View(salas.Select(s => { s.EstaNoSorteioAtual = s.SalaId == sorteio.SalaId; return s; }).OrderBy(s => s.SalaId));
        }

        // GET: Sala/Details/5
        public ActionResult Details(int id)
        {
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaService.Get(id));
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
                _salaService.Add(salaDomain);

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
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaService.Get(id));
            return View(salas);
        }

        // POST: Sala/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SalaViewModel sala)
        {
            try
            {
                _salaService.Update(_mapper.Map<SalaViewModel, Sala>(sala));
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
            var salas = _mapper.Map<Sala, SalaViewModel>(_salaService.Get(id));
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
                _salaService.Remove(_mapper.Map<SalaViewModel, Sala>(sala));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult IniciarSorteio(int SalaId)
        {
            var sala = _salaService.Get(SalaId);
            _sorteioService.Add(new Sorteio(sala));
            return RedirectToAction(nameof(Index));
        }
    }
}