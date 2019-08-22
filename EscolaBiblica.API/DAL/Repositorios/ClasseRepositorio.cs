using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class ClasseRepositorio : Repositorio<Classe>, IClasseRepositorio
    {
        public ClasseRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public IEnumerable<Classe> TodosPorCongregacao(int congregacao)
        {
            return DbSet.Where(x => x.CongregacaoId == congregacao);
        }

        public Classe ObterPorCongregacaoEId(int congregacao, int id)
        {
            return DbSet.SingleOrDefault(x => x.CongregacaoId == congregacao && x.Id == id);
        }
    }
}
