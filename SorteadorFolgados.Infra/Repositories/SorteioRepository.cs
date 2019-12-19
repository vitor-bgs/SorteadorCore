using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System;
using System.Linq;

namespace SorteadorFolgados.Infra.Repositories
{
    public class SorteioRepository : RepositoryBase<Sorteio>, ISorteioRepository
    {
        public Sorteio ObterSorteioAtual()
        {
            if (!BancoDadosFake<Sorteio>.Lista.Any())
            {
                BancoDadosFake<Sorteio>.Lista.Add(new Sorteio(new Sala() { Nome = "", QuantidadeVencedoresMaioresPontos = 0, QuantidadeVencedoresMenoresPontos = 0 })
                {
                    DataInicio = DateTime.Now
                });
            }

            return BancoDadosFake<Sorteio>.Lista.Last();
        }
    }
}
