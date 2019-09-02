using System;
using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL.Modelos;
using Microsoft.EntityFrameworkCore;

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
            return DbSet.Include(x => x.Presencas).SingleOrDefault(x => x.ClasseId == classe && x.Data == data.Date);
        }
    }
}
