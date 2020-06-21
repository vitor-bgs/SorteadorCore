using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using SorteadorCore.Domain.Entities;
using SorteadorCore.Application.Interfaces;
using SorteadorCore.Web.ViewModel;

namespace SorteadorCore.Web.Controllers
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

        [Authorize]
        public ActionResult Index()
        {
           return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var sala = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(sala);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var sala = _mapper.Map<Sala, SalaViewModel>(_salaAppService.Get(id));
            return View(sala);
        }
    }
}