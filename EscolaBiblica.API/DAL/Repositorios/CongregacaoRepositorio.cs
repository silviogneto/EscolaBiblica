using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class CongregacaoRepositorio : Repositorio<Congregacao>, ICongregacaoRepositorio
    {
        public CongregacaoRepositorio(EscolaBiblicaContext context) : base(context) { }

        public IEnumerable<Congregacao> TodosPorSetor(int setor)
        {
            return DbSet.Where(x => x.SetorNumero == setor);
        }

        public Congregacao ObterPorSetorEId(int setor, int id)
        {
            return DbSet.SingleOrDefault(x => x.SetorNumero == setor && x.Id == id);
        }
    }
}
