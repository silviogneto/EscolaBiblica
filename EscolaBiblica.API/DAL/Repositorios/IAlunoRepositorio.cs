using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IAlunoRepositorio : IRepositorio<Aluno>
    {
        IEnumerable<Aluno> TodosPorCongregacao(int congregacao);

        Aluno ObterPorCongregacaoEId(int congregacao, int id);
    }
}
