using System;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(int entity);
        List<TEntity> GetAll();
    }
}
