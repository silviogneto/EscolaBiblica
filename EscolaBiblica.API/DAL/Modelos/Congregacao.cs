using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Congregacao : ModeloBase
    {
        [Required]
        public string Nome { get; set; }

        public int SetorNumero { get; set; }
        public virtual Setor Setor { get; set; }

        public ICollection<CoordenadorCongregacao> Coordenadores { get; set; }

        public override string ToString() => Nome;
    }
}
