using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface ICoordenadorCongregacaoRepositorio : IRepositorio<CoordenadorCongregacao, (int, int)>
    {
        IEnumerable<Coordenador> ObterCoordenadoresPorCongregacaoId(int congregacaoId);

        IEnumerable<Congregacao> ObterCongregacoesPorCoordenadorId(int coordenadorId);
    }
}
