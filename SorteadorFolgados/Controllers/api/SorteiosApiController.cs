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
using SorteadorFolgados.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace SorteadorFolgados.Controllers.api
{
    [Route("api/sorteios")]
    [ApiController]
    public class SorteiosApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISalaAppService _salaAppService;
        private readonly SorteioHubConnection _sorteioHubConnection;
        private readonly ISorteioDetalheAppService _sorteioDetalheAppService;
        public SorteiosApiController(
            IMapper mapper, 
            ISorteioAppService sorteioAppService, 
            ISalaAppService salaAppService, 
            IHubContext<SorteioHub> sorteioHubContext,
            ISorteioDetalheAppService sorteioDetalheAppService)
        {
            _mapper = mapper;
            _sorteioAppService = sorteioAppService;
            _salaAppService = salaAppService;
            _sorteioDetalheAppService = sorteioDetalheAppService;
            _sorteioHubConnection = new SorteioHubConnection(sorteioHubContext);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize]
        [HttpPost("iniciar-sorteio/{SalaId}")]
        public async Task<ActionResult> IniciarSorteio(int SalaId)
        {
            try
            {
                var sala = _salaAppService.Get(SalaId);
                if (sala is null) return BadRequest("Sala inválida");
                _sorteioAppService.IniciarNovoSorteio(sala);
                var sorteioAtual = _mapper.Map<Sorteio,SorteioViewModel>(_sorteioAppService.ObterSorteioAtual());
                await _sorteioHubConnection.AtualizarSorteio(sorteioAtual);
                await _sorteioHubConnection.AtualizarVencedores();
                await _sorteioHubConnection.AvisarTodos("Sorteio " + sorteioAtual.Sala.Nome + " iniciado");
                return Ok(sorteioAtual);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("encerrar-sorteio")]
        public async Task<ActionResult> EncerrarSorteio()
        {
            try
            {
                var sorteioAtual = _sorteioAppService.ObterSorteioAtual();
                _sorteioAppService.EncerrarSorteioAtual(); 
                await _sorteioHubConnection.AtualizarSorteio(null);
                await _sorteioHubConnection.AtualizarVencedores();
                await _sorteioHubConnection.AvisarTodos("Sorteio " + sorteioAtual.Sala.Nome + " encerrado");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("obter-vencedores-sorteios/{HaNsemanas}")]
        public ActionResult<List<SorteioViewModel>> ObterVencedoresSorteios(int HaNSemanas)
        {
            try
            {
                var dataInicial = DateTime.Today.AddDays(-6).AddDays(-7*HaNSemanas);
                var dataFinal = DateTime.Today.AddDays(1).AddMilliseconds(-1).AddDays(-7*HaNSemanas);
                var sorteiosComVencedores = _sorteioAppService.ObterSorteiosComParticipacoesVencedoras(dataInicial, dataFinal);
                return Ok(_mapper.Map<List<Sorteio>, List<SorteioViewModel>>(sorteiosComVencedores));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("marcar-participacao-invalida/{SorteioDetalheId}")]
        public async Task<ActionResult> MarcarParticipacaoComoInvalida(int SorteioDetalheId)
        {
            try
            {
                _sorteioDetalheAppService.MarcarParticipacaoComoInvalida(SorteioDetalheId);
                var participacao = _sorteioDetalheAppService.Get(SorteioDetalheId);
                var sorteio = _sorteioAppService.ObterSorteioAtual();
                await _sorteioHubConnection.AtualizarSorteio(_mapper.Map<Sorteio, SorteioViewModel>(sorteio));
                await _sorteioHubConnection.AtualizarVencedores();
                await _sorteioHubConnection.AvisarTodos($"A participação [{participacao.Sorteio.Sala}] {participacao.Participante.Nome} {participacao.Pontos} foi marcada como inválida");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("marcar-participacao-valida/{SorteioDetalheId}")]
        public async Task<ActionResult> MarcarParticipacaoComoValida(int SorteioDetalheId)
        {
            try
            {
                _sorteioDetalheAppService.MarcarParticipacaoComoInvalida(SorteioDetalheId);
                var participacao = _sorteioDetalheAppService.Get(SorteioDetalheId);
                var sorteio = _sorteioAppService.ObterSorteioAtual();
                await _sorteioHubConnection.AtualizarSorteio(_mapper.Map<Sorteio, SorteioViewModel>(sorteio));
                await _sorteioHubConnection.AtualizarVencedores();
                await _sorteioHubConnection.AvisarTodos($"A participação [{participacao.Sorteio.Sala}] {participacao.Participante.Nome} {participacao.Pontos} foi marcada como válida");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}