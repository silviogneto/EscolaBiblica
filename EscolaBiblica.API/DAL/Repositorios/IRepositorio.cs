using System.Collections.Generic;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IRepositorio<TModelo, TPK>
    {
        IEnumerable<TModelo> Todos();
        TModelo ObterPorId(TPK id);
        void Adicionar(TModelo entidade);
        void Alterar(TModelo entidade);
        void Excluir(TModelo entidade);
        void Excluir(TPK id);
    }
}
