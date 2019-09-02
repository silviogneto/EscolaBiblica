using System;

namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CongregacaoId { get; set; }

        public override string ToString() => Nome;

        public override bool Equals(object obj)
        {
            var o = obj as AlunoDTO;
            if (o == null)
                return false;

            return Id == o.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
