using System.Collections.Generic;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Professor : Aluno
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<ProfessorClasse> Classes { get; set; }
    }
}
