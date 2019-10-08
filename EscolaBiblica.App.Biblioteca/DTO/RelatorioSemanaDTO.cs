using System;

namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class RelatorioSemanaDTO
    {
        public DateTime InicioSemana { get; set; }
        public DateTime FimSemana { get; set; }

        public int Matriculados { get; set; }
        public int Ausentes { get; set; }
        public int Presentes { get; set; }
        public int Visitantes { get; set; }
        public decimal Oferta { get; set; }
    }
}
