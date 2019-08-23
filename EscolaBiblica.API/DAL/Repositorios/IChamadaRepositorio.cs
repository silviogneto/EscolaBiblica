using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IChamadaRepositorio : IRepositorio<Chamada>
    {
        IEnumerable<Chamada> TodosPorClasse(int classe);
        Chamada ObterPorClasseEData(int classe, DateTime data);
    }
}
