﻿using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface ISorteioService : IServiceBase<Sorteio>
    {
        Sorteio ObterSorteioAtual();
    }
}