using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface ICongregacaoRepositorio : IRepositorio<Congregacao>
    {
        IEnumerable<Congregacao> TodosPorSetor(int setor);

        Congregacao ObterPorSetorEId(int setor, int id);
    }
}
