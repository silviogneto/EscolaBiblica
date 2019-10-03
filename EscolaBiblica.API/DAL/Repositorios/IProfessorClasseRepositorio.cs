using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IProfessorClasseRepositorio : IRepositorio<ProfessorClasse, (int, int)>
    {
        IEnumerable<Classe> ObterClassesPorProfessorId(int professorId);

        IEnumerable<Professor> ObterProfessoresPorClasseId(int classeId);
    }
}
