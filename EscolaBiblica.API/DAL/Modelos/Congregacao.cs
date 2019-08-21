using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Congregacao : ModeloBase
    {
        [Required]
        public string Nome { get; set; }

        public int SetorNumero { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
