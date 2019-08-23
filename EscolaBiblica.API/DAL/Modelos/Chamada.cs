using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Chamada : ModeloBase
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public int Visitantes { get; set; }
        public int Biblias { get; set; }
        public int Revistas { get; set; }
        public decimal Oferta { get; set; }

        public virtual IEnumerable<Presenca> Presencas { get; set; }

        public int ClasseId { get; set; }
        public virtual Classe Classe { get; set; }
    }
}
