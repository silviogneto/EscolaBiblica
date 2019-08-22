using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class AlunoRepositorio : Repositorio<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public IEnumerable<Aluno> TodosPorCongregacao(int congregacao)
        {
            return DbSet.Where(x => x.CongregacaoId == congregacao);
        }

        public Aluno ObterPorCongregacaoEId(int congregacao, int id)
        {
            return DbSet.SingleOrDefault(x => x.CongregacaoId == congregacao && x.Id == id);
        }
    }
}
