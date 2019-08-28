using System.Linq;
using EscolaBiblica.API.DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class ProfessorRepositorio : Repositorio<Professor>, IProfessorRepositorio
    {
        public ProfessorRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public Professor ObterPorUsuario(int usuario)
        {
            return DbSet.Include(x => x.Congregacao)
                        .ThenInclude(x => x.Setor)
                        .FirstOrDefault(x => x.UsuarioId == usuario);
        }
    }
}
