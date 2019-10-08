using System.Collections.Generic;

namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class RelatorioDTO
    {
        public int Setor { get; set; }
        public int Congregacao { get; set; }
        public string NomeCongregacao { get; set; }
        public decimal OfertaMes { get; set; }
        public decimal OfertaDepartamento { get; set; }

        public IEnumerable<RelatorioSemanaDTO> Semanas { get; set; }
    }
}
