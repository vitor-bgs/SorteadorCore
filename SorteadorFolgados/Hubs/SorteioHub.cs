using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorteadorFolgados.ViewModel;
using Microsoft.AspNetCore.SignalR;
using SorteadorFolgados.Application.Interfaces;
using AutoMapper;
using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Hubs
{
    public class SorteioHub : Hub
    {
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISorteioDetalheAppService _sorteioDetalheAppService;
        private readonly IMapper _mapper;
        public SorteioHub(IMapper mapper, ISorteioAppService sorteioAppService, ISorteioDetalheAppService sorteioDetalheAppService)
        {
            _mapper = mapper;
            _sorteioAppService = sorteioAppService;
            _sorteioDetalheAppService = sorteioDetalheAppService;
        }

        public override Task OnConnectedAsync()
        {
            Sorteio sorteioAtual = _sorteioAppService.ObterSorteioAtual();
            if(sorteioAtual != null)
            {
                Clients.Caller.SendAsync("atualizarSorteio", _mapper.Map<Sorteio, SorteioViewModel>(sorteioAtual));
            }
            return base.OnConnectedAsync();
        }

        public async Task Sortear(string nomeParticipante)
        {
            try
            {
                Sorteio sorteioAtual = _sorteioAppService.ObterSorteioAtual();
                if (sorteioAtual == null)
                {
                    await Clients.Caller.SendAsync("error", "Não há sorteio em aberto");
                    return;
                }
                string EnderecoIP = Context.GetHttpContext().Connection.RemoteIpAddress.MapToIPv4().ToString();
                _sorteioDetalheAppService.Sortear(nomeParticipante, EnderecoIP);
                await Clients.All.SendAsync("atualizarSorteio", _mapper.Map<Sorteio, SorteioViewModel>(sorteioAtual));
                var participacao = _sorteioDetalheAppService.GetSorteioDetalhes(sorteioAtual.SorteioId).First(p => p.Participante.Nome == nomeParticipante);
                await Clients.Caller.SendAsync("sortearOk", _mapper.Map<SorteioDetalhe, SorteioDetalheViewModel>(participacao));
            }
            catch
            {
                await Clients.Caller.SendAsync("error", "Não há sorteio em aberto");
            }
        }
    }
}
