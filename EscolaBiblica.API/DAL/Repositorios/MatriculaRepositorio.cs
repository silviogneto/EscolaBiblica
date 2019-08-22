using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class MatriculaRepositorio : Repositorio<Matricula>, IMatriculaRepositorio
    {
        public MatriculaRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }
    }
}
