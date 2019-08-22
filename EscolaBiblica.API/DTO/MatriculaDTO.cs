using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DTO
{
    public class MatriculaDTO
    {
        [Required]
        public int AlunoId { get; set; }
    }
}
