using System.Collections.Generic;
using System.Linq;
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

        public virtual IEnumerable<TModelo> Todos() => DbSet;

        public abstract TModelo ObterPorId(TPK id);

        public virtual void Adicionar(TModelo entidade) => DbSet.Add(entidade);

        public virtual void Alterar(TModelo entidade) => DbSet.Update(entidade);

        public virtual void Excluir(TModelo entidade) => DbSet.Remove(entidade);

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
