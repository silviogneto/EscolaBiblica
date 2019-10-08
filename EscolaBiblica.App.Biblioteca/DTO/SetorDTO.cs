using System.Collections.Generic;

namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class SetorDTO
    {
        public int Numero { get; set; }
        public string Nome { get; set; }

        public IEnumerable<CongregacaoDTO> Congregacoes { get; set; }

        public override string ToString() => $"Setor: {Numero.ToString("00")} - {Nome}";
    }
}
