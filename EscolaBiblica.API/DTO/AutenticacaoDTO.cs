using System;
using System.Collections.Generic;

namespace EscolaBiblica.API.DTO
{
    public class AutenticacaoDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string Perfil { get; set; }
        public IEnumerable<ClasseDTO> Classes { get; set; }
    }
}
