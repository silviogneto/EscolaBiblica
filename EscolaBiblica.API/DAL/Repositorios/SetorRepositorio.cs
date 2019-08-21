using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class SetorRepositorio : Repositorio<Setor, int>, ISetorRepositorio
    {
        public SetorRepositorio(EscolaBiblicaContext context) : base(context) { }

        public override Setor ObterPorId(int id) => DbSet.SingleOrDefault(x => x.Numero == id);
    }
}
