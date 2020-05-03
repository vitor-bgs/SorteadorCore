using Microsoft.AspNetCore.SignalR;
using SorteadorFolgados.Hubs;
using SorteadorFolgados.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorFolgados.Controllers.api
{
    public class SorteioHubConnection
    {
        private readonly IHubContext<SorteioHub> _sorteioHubContext;
        public SorteioHubConnection(
            IHubContext<SorteioHub> sorteioHubContext)
        {
            _sorteioHubContext = sorteioHubContext;
        }

        public async Task AtualizarSorteio(SorteioViewModel sorteio)
        {
            await _sorteioHubContext.Clients.All.SendAsync("atualizarSorteio", sorteio);
        }

        public async Task AtualizarVencedores()
        {
            await _sorteioHubContext.Clients.All.SendAsync("atualizarVencedores");
        }

        public async Task AvisarTodos(string aviso)
        {
            await _sorteioHubContext.Clients.All.SendAsync("aviso", aviso);
        }
    }
}
