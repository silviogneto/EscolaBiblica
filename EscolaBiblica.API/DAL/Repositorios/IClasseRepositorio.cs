using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IClasseRepositorio : IRepositorio<Classe>
    {
        IEnumerable<Classe> TodosPorCongregacao(int congregacao);

        Classe ObterPorCongregacaoEId(int congregacao, int id);
    }
}
