using SorteadorCore.Domain.Entities;
using System.Collections.Generic;

namespace SorteadorCore.Application.Interfaces
{
    public interface ISorteioDetalheAppService : IAppServiceBase<SorteioDetalhe>
    {
        List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId);
        void Sortear(string nomeParticipante, string EnderecoIP);
        void MarcarParticipacaoComoValida(int sorteioDetalheId);
        void MarcarParticipacaoComoInvalida(int sorteioDetalheId);
    }
}
