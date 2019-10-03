using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class ProfessorClasseRepositorio : Repositorio<ProfessorClasse, (int, int)>, IProfessorClasseRepositorio
    {
        public ProfessorClasseRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public override ProfessorClasse ObterPorId((int, int) id) => DbSet.SingleOrDefault(x => x.ProfessorId == id.Item1 && x.ClasseId == id.Item2);

        public IEnumerable<Classe> ObterClassesPorProfessorId(int professorId) => DbSet.Where(x => x.ProfessorId == professorId).Select(x => x.Classe).Include(x => x.Congregacao);

        public IEnumerable<Professor> ObterProfessoresPorClasseId(int classeId) => DbSet.Where(x => x.ClasseId == classeId).Select(x => x.Professor);
    }
}
