using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Services;
using SorteadorFolgados.Infra.Repositories;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers
{
    public class SalaController : Controller
    {
        private readonly IMapper _mapper;

        public SalaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: Sala
        public ActionResult Index()
        {
            var salaService = new SalaService(new SalaRepository());
            var salas = salaService.GetAll().Select(s => _mapper.Map<Sala, SalaViewModel>(s));
            return View(salas);
        }

        // GET: Sala/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                var salaService = new SalaService(new SalaRepository());
                salaService.Add(salaDomain);

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
            return View();
        }

        // POST: Sala/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Sala/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}