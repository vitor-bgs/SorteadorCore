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

        public SalasController(
            IMapper mapper, 
            ISalaAppService salaAppService)
        {
            _mapper = mapper;
            _salaAppService = salaAppService;
        }

        public ActionResult Index()
        {
           return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var sala = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(sala);
        }
        public ActionResult Delete(int id)
        {
            var sala = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(sala);
        }
    }
}