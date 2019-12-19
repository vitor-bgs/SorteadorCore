using System.Collections.Generic;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;

namespace SorteadorFolgados.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _repository.Add(entity);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public TEntity Get(int entity)
        {
            return _repository.Get(entity);
        }

        public List<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}
