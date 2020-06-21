using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Application.Interfaces
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(int entity);
        List<TEntity> GetAll();
    }
}
