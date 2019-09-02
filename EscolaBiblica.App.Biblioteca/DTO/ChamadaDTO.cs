using System.Collections.Generic;

namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class ChamadaDTO
    {
        public int? Visitantes { get; set; }
        public int? Biblias { get; set; }
        public int? Revistas { get; set; }
        public decimal? Oferta { get; set; }

        public IEnumerable<AlunoDTO> Presencas { get; set; }

        public IEnumerable<AlunoDTO> Matriculados { get; set; }
    }
}
