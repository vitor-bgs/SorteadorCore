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

        public async override Task OnConnectedAsync()
        {
            Sorteio sorteioAtual = _sorteioAppService.ObterSorteioAtual();
            if(sorteioAtual is null)
            {
                await Clients.Caller.SendAsync("aviso", "Não há sorteio ativo no momento");
                await AtualizarSorteio(null);
                await base.OnConnectedAsync();
            }
            await Clients.Caller.SendAsync("atualizarSorteio", _mapper.Map<Sorteio, SorteioViewModel>(sorteioAtual));
            await base.OnConnectedAsync();
        }

        public async Task Sortear(string nomeParticipante)
        {
            try
            {
                Sorteio sorteioAtual = _sorteioAppService.ObterSorteioAtual();
                if (sorteioAtual is null)
                {
                    await Avisar("Não há sorteio ativo no momento");
                    await AtualizarSorteio(null);
                    return;
                }

                if(sorteioAtual.Participacoes.Any(p => p.Participante.Nome == nomeParticipante))
                {
                    var participacaoRepetida = sorteioAtual.Participacoes.First(p => p.Participante.Nome == nomeParticipante);
                    await Avisar(participacaoRepetida.Participante.Nome + " já está participando com " + participacaoRepetida.Pontos + " pontos");
                    return;
                }

                string EnderecoIP = Context.GetHttpContext().Connection.RemoteIpAddress.MapToIPv4().ToString();
                _sorteioDetalheAppService.Sortear(nomeParticipante, EnderecoIP);
                await AtualizarSorteio(_mapper.Map<Sorteio, SorteioViewModel>(sorteioAtual));
                var participacao = _sorteioDetalheAppService.GetSorteioDetalhes(sorteioAtual.SorteioId).First(p => p.Participante.Nome == nomeParticipante);
                await Avisar(participacao.Participante.Nome + " sorteou o número " + participacao.Pontos);
            }
            catch
            {
                await Avisar("Houve um erro ao sortear");
            }
        }

        public async Task AtualizarSorteio(SorteioViewModel sorteioAtual)
        {
            await Clients.All.SendAsync("atualizarSorteio", sorteioAtual);
        }

        public async Task AvisarTodos(string mensagem)
        {
            await Clients.All.SendAsync("aviso", mensagem);
        }

        private async Task Avisar(string mensagem)
        {
            await Clients.Caller.SendAsync("aviso", mensagem);
        }
    }
}
