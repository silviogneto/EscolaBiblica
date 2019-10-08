using System;
using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class MatriculaRepositorio : Repositorio<Matricula>, IMatriculaRepositorio
    {
        public MatriculaRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public IEnumerable<Matricula> TodosPorClassesESemana(IEnumerable<int> idsClasses, DateTime inicio, DateTime fim)
        {
            return DbSet.Where(x => idsClasses.Contains(x.ClasseId) &&
                                    x.DataMatricula >= inicio &&
                                    (x.DataTerminoMatricula == null || x.DataTerminoMatricula <= fim));
        }
    }
}
