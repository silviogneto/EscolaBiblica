using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IRepositorio<TModelo, TPK>
    {
        IEnumerable<TModelo> Todos();
        IEnumerable<TModelo> Todos(Expression<Func<TModelo, bool>> where = null, Func<IQueryable<TModelo>, IOrderedQueryable<TModelo>> orderBy = null, Expression<Func<TModelo, object>> include = null);
        TModelo ObterPorId(TPK id);
        void Adicionar(TModelo entidade);
        void Adicionar(IEnumerable<TModelo> entidades);
        void Alterar(TModelo entidade);
        void Alterar(IEnumerable<TModelo> entidades);
        void Excluir(TModelo entidade);
        void Excluir(TPK id);
    }

    public interface IRepositorio<TModelo> : IRepositorio<TModelo, int>
    {
    }
}
