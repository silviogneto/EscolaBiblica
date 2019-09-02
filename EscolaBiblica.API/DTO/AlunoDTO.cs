using System;

namespace EscolaBiblica.API.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CongregacaoId { get; set; }
    }
}
