using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class PresencaRepositorio : Repositorio<Presenca>, IPresencaRepositorio
    {
        public PresencaRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }
    }
}
