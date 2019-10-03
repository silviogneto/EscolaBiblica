using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EscolaBiblica.API.DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public abstract class Repositorio<TModelo, TPK> : IRepositorio<TModelo, TPK> where TModelo : class
    {
        public DbSet<TModelo> DbSet { get; private set; }

        public Repositorio(EscolaBiblicaContext context)
        {
            DbSet = context.Set<TModelo>();
        }

        public virtual IEnumerable<TModelo> Todos(Expression<Func<TModelo, bool>> where = null, Func<IQueryable<TModelo>, IOrderedQueryable<TModelo>> orderBy = null, Expression<Func<TModelo, object>> include = null)
        {
            var dbSet = DbSet.AsQueryable();

            if (include != null)
                dbSet = dbSet.Include(include);

            if (where != null)
                dbSet = dbSet.Where(where);

            if (orderBy != null)
                dbSet = orderBy(dbSet);

            return dbSet;
        }

        public abstract TModelo ObterPorId(TPK id);

        public virtual void Adicionar(TModelo entidade) => DbSet.Add(entidade);

        public virtual void Adicionar(IEnumerable<TModelo> entidades) => DbSet.AddRange(entidades);

        public virtual void Alterar(TModelo entidade) => DbSet.Update(entidade);

        public virtual void Alterar(IEnumerable<TModelo> entidades) => DbSet.UpdateRange(entidades);

        public virtual void Excluir(TModelo entidade) => DbSet.Remove(entidade);

        public virtual void Excluir(IEnumerable<TModelo> entidades) => DbSet.RemoveRange(entidades);

        public void Excluir(TPK id)
        {
            var entidade = ObterPorId(id);
            if (entidade != null)
                Excluir(entidade);
        }
    }

    public abstract class Repositorio<TModelo> : Repositorio<TModelo, int> where TModelo : ModeloBase
    {
        public Repositorio(EscolaBiblicaContext context) : base(context) { }

        public override TModelo ObterPorId(int id) => DbSet.SingleOrDefault(x => x.Id == id);
    }
}
