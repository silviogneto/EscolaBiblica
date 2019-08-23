using System.ComponentModel.DataAnnotations;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Usuario : ModeloBase
    {
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Perfil { get; set; }
        public bool Ativo { get; set; }

        public override string ToString() => $"{Id} - {Nome}";
    }
}
