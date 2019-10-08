using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IClasseRepositorio : IRepositorio<Classe>
    {
        IEnumerable<Classe> TodosPorCongregacao(int congregacao);

        IEnumerable<Classe> TodosPorCongregacoes(IEnumerable<Congregacao> congregacoes);

        Classe ObterPorCongregacaoEId(int congregacao, int id);
    }
}
