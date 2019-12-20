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

        public override Sorteio Add(Sorteio entity)
        {
            entity.SorteioId = BancoDadosFake<Sorteio>.Lista.Count + 1;
            return base.Add(entity);
        }

        public override Sorteio Get(int entity)
        {
            return BancoDadosFake<Sorteio>.Lista.FirstOrDefault(p => p.SorteioId == entity);
        }
    }
}
