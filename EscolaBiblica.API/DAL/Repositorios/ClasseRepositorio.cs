using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class ClasseRepositorio : Repositorio<Classe>, IClasseRepositorio
    {
        public ClasseRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public IEnumerable<Classe> TodosPorCongregacao(int congregacao) => DbSet.Where(x => x.CongregacaoId == congregacao).Include(x => x.Congregacao);

        public IEnumerable<Classe> TodosPorCongregacoes(IEnumerable<Congregacao> congregacoes)
        {
            var ids = congregacoes.Select(x => x.Id);

            return DbSet.Where(x => ids.Contains(x.CongregacaoId)).Include(x => x.Congregacao);
        }

        public Classe ObterPorCongregacaoEId(int congregacao, int id) => DbSet.SingleOrDefault(x => x.CongregacaoId == congregacao && x.Id == id);
    }
}
