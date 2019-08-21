using System.ComponentModel.DataAnnotations;
using EscolaBiblica.API.Enumeradores;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Endereco : ModeloBase
    {
        [Required]
        public string CEP { get; set; }
        [Required]
        public string DescEndereco { get; set; }
        [Required]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public TipoEndereco TipoEndereco { get; set; }

        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
