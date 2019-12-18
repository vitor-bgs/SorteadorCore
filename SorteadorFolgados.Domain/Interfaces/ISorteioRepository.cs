using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Domain.Interfaces
{
    public interface ISorteioRepository
    {
        Sorteio AdicionarSorteio(Sorteio sorteio);
        Sorteio ObterSorteioAtual();
        Participante SortearParticipante(Participante participante, int SorteioId);

    }
}
