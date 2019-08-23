using System.Collections.Generic;

namespace EscolaBiblica.API.DTO
{
    public class ChamadaDTO
    {
        public int Visitantes { get; set; }
        public int Biblias { get; set; }
        public int Revistas { get; set; }
        public decimal Oferta { get; set; }

        public IEnumerable<PresencaDTO> Presencas { get; set; }
    }
}
