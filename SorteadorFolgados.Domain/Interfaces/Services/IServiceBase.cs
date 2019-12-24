using System;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        void Remove(TEntity obj);
        void Update(TEntity obj);
        TEntity Get(int entityId);
        List<TEntity> GetAll();
    }
}
