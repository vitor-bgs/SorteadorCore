using System.Collections.Generic;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Domain.Interfaces.Services;

namespace SorteadorCore.Domain.Services
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

        public virtual TEntity Get(int entityId)
        {
            return _repository.Get(entityId);
        }

        public virtual List<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public virtual void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
    }
}
