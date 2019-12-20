using System;
using System.Collections.Generic;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;

namespace SorteadorFolgados.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public virtual TEntity Add(TEntity entity)
        {
            BancoDadosFake<TEntity>.Lista.Add(entity);
            return entity;
        }

        public void Dispose()
        {
        }

        public virtual TEntity Get(int entityId)
        {
            throw new Exception("Not Implemented");
        }

        public virtual List<TEntity> GetAll()
        {
            return BancoDadosFake<TEntity>.Lista;
        }

        public virtual void Remove(TEntity entity)
        {
            BancoDadosFake<TEntity>.Lista.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            throw new Exception("Not Implemented");
        }
    }
}
