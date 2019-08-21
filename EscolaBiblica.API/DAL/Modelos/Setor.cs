using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Setor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Numero { get; set; }
        [Required]
        public string Nome { get; set; }

        public override string ToString() => $"Setor {Numero} - {Nome}";
    }
}
