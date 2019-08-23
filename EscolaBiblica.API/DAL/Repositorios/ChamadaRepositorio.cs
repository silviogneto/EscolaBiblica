using System;
using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class ChamadaRepositorio : Repositorio<Chamada>, IChamadaRepositorio
    {
        public ChamadaRepositorio(EscolaBiblicaContext context) : base(context)
        {
        }

        public IEnumerable<Chamada> TodosPorClasse(int classe) => DbSet.Where(x => x.ClasseId == classe);

        public Chamada ObterPorClasseEData(int classe, DateTime data)
        {
            return DbSet.SingleOrDefault(x => x.ClasseId == classe && x.Data == data.Date);
        }
    }
}
