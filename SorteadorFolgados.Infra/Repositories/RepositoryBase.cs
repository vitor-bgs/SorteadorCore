using System.Collections.Generic;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;

namespace SorteadorFolgados.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public TEntity Add(TEntity entity)
        {
            BancoDadosFake<TEntity>.Lista.Add(entity);
            return entity;
        }

        public void Dispose()
        {
        }

        public TEntity Get(int entity)
        {
            return BancoDadosFake<TEntity>.Lista[entity];
        }

        public List<TEntity> GetAll()
        {
            return BancoDadosFake<TEntity>.Lista;
        }

        public void Remove(TEntity entity)
        {
            BancoDadosFake<TEntity>.Lista.Remove(entity);
        }

        public void Update(TEntity entity)
        {

        }
    }
}
