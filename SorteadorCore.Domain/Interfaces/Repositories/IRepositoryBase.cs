using System;
using System.Collections.Generic;

namespace SorteadorCore.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(int entity);
        List<TEntity> GetAll();
    }
}
