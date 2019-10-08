using System.Collections.Generic;

namespace EscolaBiblica.API.DAL.Modelos
{
    public class Coordenador : Professor
    {
        public ICollection<CoordenadorCongregacao> Congregacoes { get; set; }
    }
}
