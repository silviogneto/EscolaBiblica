using System;
using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IMatriculaRepositorio : IRepositorio<Matricula>
    {
        IEnumerable<Matricula> TodosPorClassesESemana(IEnumerable<int> idsClasses, DateTime inicio, DateTime fim);
    }
}
