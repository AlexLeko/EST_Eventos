using System;
using EventosIO.Domain.Core.Models;
using EventosIO.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using EventoIO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventoIO.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected EventosContext DB;
        protected DbSet<TEntity> DbSet;

        protected Repository(EventosContext context)
        {
            DB = context;
            DbSet = DB.Set<TEntity>();
        }


        #region [ACTION]

        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return DB.SaveChanges();
        }

        public void Dispose()
        {
            DB.Dispose();
        }

        #endregion [ACTION]

    }
}
