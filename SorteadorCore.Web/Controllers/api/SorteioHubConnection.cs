using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

using SorteadorCore.Web.Hubs;
using SorteadorCore.Web.ViewModel;

namespace SorteadorCore.Web.Controllers.api
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
