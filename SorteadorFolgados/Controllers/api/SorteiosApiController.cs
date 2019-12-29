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
    [Route("api/sorteios")]
    [ApiController]
    public class SorteiosApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISorteioDetalheAppService _sorteioDetalheAppService;
        private readonly ISalaAppService _salaAppService;

        public SorteiosApiController(IMapper mapper, ISorteioAppService sorteioAppService, ISorteioDetalheAppService sorteioDetalheAppService, ISalaAppService salaAppService)
        {
            _mapper = mapper;
            _sorteioAppService = sorteioAppService;
            _sorteioDetalheAppService = sorteioDetalheAppService;
            _salaAppService = salaAppService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SorteioViewModel>> Sorteios()
        {
            try
            {
                var sorteios = _sorteioAppService.GetAll()
                    .Select(s => _mapper.Map<Sorteio, SorteioViewModel>(s));
                return Ok(sorteios);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{SorteioId}")]
        public ActionResult<SorteioViewModel> Sorteios(int SorteioId)
        {
            try
            {
                var sorteio = _sorteioAppService.Get(SorteioId);
                if (sorteio is null) return NotFound();
                return Ok(_mapper.Map<Sorteio, SorteioViewModel>(sorteio));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("iniciar-sorteio")]
        public ActionResult IniciarSorteio([FromBody]int SalaId)
        {
            try
            {
                var sala = _salaAppService.Get(SalaId);
                if (sala is null) return BadRequest("Sala inválida");
                _sorteioAppService.IniciarNovoSorteio(sala);
                return Ok(_sorteioAppService.ObterSorteioAtual());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("encerrar-sorteio")]
        public ActionResult EncerrarSorteio()
        {
            try
            {
                _sorteioAppService.EncerrarSorteioAtual();
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}