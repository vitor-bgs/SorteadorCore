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

        public virtual TEntity Add(TEntity obj)
        {
            return _repository.Add(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public TEntity Get(int entityId)
        {
            return _repository.Get(entityId);
        }

        public List<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
    }
}
