using SorteadorCore.Application.Interfaces;
using SorteadorCore.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace SorteadorCore.Application
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public TEntity Add(TEntity obj)
        {
            return _serviceBase.Add(obj);
        }

        public TEntity Get(int entityId)
        {
            return _serviceBase.Get(entityId);
        }

        public List<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }
    }
}
