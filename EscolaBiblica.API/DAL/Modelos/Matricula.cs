using System;
using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Matricula : ModeloBase
    {
        [Required]
        public DateTime DataMatricula { get; set; }
        public DateTime? DataTerminoMatricula { get; set; }

        [Required]
        public int ClasseId { get; set; }
        public virtual Classe Classe { get; set; }

        [Required]
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
