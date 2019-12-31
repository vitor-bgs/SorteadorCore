using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.ViewModel;

namespace SorteadorFolgados.Controllers.api
{
    [Route("api/salas")]
    [ApiController]
    public class SalasApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISalaAppService _salaAppService;
        private readonly ISorteioAppService _sorteioAppService;

        public SalasApiController(
            IMapper mapper, 
            ISalaAppService salaAppService, 
            ISorteioAppService sorteioAppService)
        {
            _mapper = mapper;
            _salaAppService = salaAppService;
            _sorteioAppService = sorteioAppService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SalaViewModel>> Salas()
        {
            var salas = _salaAppService.GetAll()
                .Where(s => s.Ativo)
                .Select(s => _mapper.Map<Sala, SalaViewModel>(s));
            var sorteio = _sorteioAppService.ObterSorteioAtual();
            if (sorteio == null)
            {
                return Ok(salas.OrderBy(s => s.SalaId));
            }
            return Ok(
                salas.Select(s => { s.EstaNoSorteioAtual = s.SalaId == sorteio.SalaId; return s; })
                .OrderBy(s => s.SalaId)
                );
        }

        [HttpGet("{id}")]
        public ActionResult<SalaViewModel> Salas(int id)
        {
            try
            {
                var sala = _salaAppService.Get(id);
                if (sala is null) return NotFound();
                return Ok(_mapper.Map<Sala, SalaViewModel>(sala));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<SalaViewModel> Create([FromBody] SalaViewModel sala)
        {
            try
            {
                var salaDomain = _mapper.Map<SalaViewModel, Sala>(sala);
                var salaAdicionada = _salaAppService.Add(salaDomain);
                return Ok(_mapper.Map<Sala, SalaViewModel>(salaAdicionada));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        public ActionResult Edit(int id, [FromBody] SalaViewModel sala)
        {
            try
            {
                if (id != sala.SalaId) return BadRequest();
                _salaAppService.Update(_mapper.Map<SalaViewModel, Sala>(sala));
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromBody] SalaViewModel sala)
        {
            try
            {
                if (sala.SalaId != id) return BadRequest();
                _salaAppService.Remove(_mapper.Map<SalaViewModel, Sala>(sala));
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}