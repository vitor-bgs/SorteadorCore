using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;

namespace SorteadorFolgados.Infra.Repositories
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> where TEntity : class where TContext : DbContext
    {
        protected DbSet<TEntity> DbSet;
        protected readonly TContext Db;
        
        public RepositoryBase(TContext db){
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            var obji = DbSet.Add(obj);
            Db.SaveChanges();
            return obji.Entity;
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public virtual TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
            Db.SaveChanges();
            
        }

        public virtual void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
