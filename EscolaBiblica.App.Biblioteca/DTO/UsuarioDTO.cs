using System;
using System.Collections.Generic;

namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string Perfil { get; set; }

        public IEnumerable<SetorDTO> Setores { get; set; }
    }
}