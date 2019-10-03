using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Classe : ModeloBase
    {
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Required]
        public int CongregacaoId { get; set; }
        public Congregacao Congregacao { get; set; }

        public ICollection<ProfessorClasse> Professores { get; set; }

        public override string ToString() => Nome;
    }
}
