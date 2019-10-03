using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Presenca : ModeloBase
    {
        [Required]
        public int IdAluno { get; set; }
        [Required]
        public string NomeAluno { get; set; }

        public int ChamadaId { get; set; }
        public Chamada Chamada { get; set; }
    }
}
