using System.Collections.Generic;

namespace EscolaBiblica.API.DTO
{
    public class RelatorioDTO
    {
        public int Setor { get; set; }
        public int Congregacao { get; set; }
        public string NomeCongregacao { get; set; }
        public decimal OfertaMes { get; set; }
        public decimal OfertaDepartamento { get; set; }
        public List<RelatorioSemanaDTO> Semanas { get; set; }
    }
}
