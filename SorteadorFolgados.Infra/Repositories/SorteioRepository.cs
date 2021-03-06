﻿using Microsoft.EntityFrameworkCore;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SorteadorFolgados.Infra.Repositories
{
    public class SorteioRepository : RepositoryBase<Sorteio, BancoContexto>, ISorteioRepository
    {
        public SorteioRepository(BancoContexto db) : base(db)
        {
        }

        public Sorteio ObterSorteioAtual()
        {
            return Db.Sorteios.Include(s => s.Sala).Include(s => s.Participacoes).ThenInclude(p => p.Participante).FirstOrDefault(s => s.Ativo);
        }

        public List<Sorteio> ObterSorteiosPorData(DateTime dataInicial, DateTime dataFinal)
        {
            return DbSet.Include(s => s.Sala).Include(s => s.Participacoes).ThenInclude(p => p.Participante).Where(s => s.DataInicio >= dataInicial && s.DataInicio <= dataFinal).ToList();
        }
    }
}
