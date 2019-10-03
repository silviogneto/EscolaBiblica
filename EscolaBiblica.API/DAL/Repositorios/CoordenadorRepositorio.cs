using System.Linq;
using EscolaBiblica.API.DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class CoordenadorRepositorio : Repositorio<Coordenador>, ICoordenadorRepositorio
    {
        public CoordenadorRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public Coordenador ObterPorUsuario(int usuario)
        {
            return DbSet.Include(x => x.Congregacao)
                        .ThenInclude(x => x.Setor)
                        .SingleOrDefault(x => x.UsuarioId == usuario);
        }
    }
}
