using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class CoordenadorCongregacaoRepositorio : Repositorio<CoordenadorCongregacao, (int, int)>, ICoordenadorCongregacaoRepositorio
    {
        public CoordenadorCongregacaoRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public override CoordenadorCongregacao ObterPorId((int, int) id) => DbSet.SingleOrDefault(x => x.CoordenadorId == id.Item1 && x.CongregacaoId == id.Item2);

        public IEnumerable<Congregacao> ObterCongregacoesPorCoordenadorId(int coordenadorId) => DbSet.Where(x => x.CoordenadorId == coordenadorId).Select(x => x.Congregacao);

        public IEnumerable<Coordenador> ObterCoordenadoresPorCongregacaoId(int congregacaoId) => DbSet.Where(x => x.CongregacaoId == congregacaoId).Select(x => x.Coordenador);
    }
}
