using System;
using System.Collections.Generic;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IChamadaRepositorio : IRepositorio<Chamada>
    {
        IEnumerable<Chamada> TodosPorClasse(int classe);
        Chamada ObterPorClasseEData(int classe, DateTime data);
        IEnumerable<Chamada> TodosPorSetorCongregacaoAnoMes(int setor, int congregacao, int ano, int mes);
    }
}
