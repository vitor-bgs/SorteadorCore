using SorteadorFolgados.Domain.Entities;
using System.Collections.Generic;

namespace SorteadorFolgados.Application.Interfaces
{
    public interface ISorteioDetalheAppService : IAppServiceBase<SorteioDetalhe>
    {
        List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId);
        void Sortear(string nomeParticipante, string EnderecoIP);
        void MarcarParticipacaoComoValida(int sorteioDetalheId);
        void MarcarParticipacaoComoInvalida(int sorteioDetalheId);
    }
}
