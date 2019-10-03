using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Matricula : ModeloBase
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime DataMatricula { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DataTerminoMatricula { get; set; }

        [Required]
        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        [Required]
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
    }
}
