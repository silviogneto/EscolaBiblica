using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Aluno : ModeloBase
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }

        public int CongregacaoId { get; set; }
        public Congregacao Congregacao { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }

        public override string ToString() => Nome;
    }
}
