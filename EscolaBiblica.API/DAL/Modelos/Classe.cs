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
        public virtual Congregacao Congregacao { get; set; }
    }
}
