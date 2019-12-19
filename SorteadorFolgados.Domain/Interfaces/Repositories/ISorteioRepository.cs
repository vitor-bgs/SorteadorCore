using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Domain.Interfaces.Repository
{
    public interface ISorteioRepository :IRepositoryBase<Sorteio>
    {
        Sorteio ObterSorteioAtual();
    }
}
