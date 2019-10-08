using System;
using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL;
using EscolaBiblica.API.DTO;
using EscolaBiblica.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaBiblica.API.Controllers
{
    public class RelatoriosController : BaseController<RelatorioDTO>
    {
        public RelatoriosController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador)]
        [HttpGet]
        [Route("~/api/Setores/{setor}/Congregacoes/{congregacao}/[controller]/Ano/{ano}/Mes/{mes}")]
        public IActionResult Get(int setor, int congregacao, int ano, int mes)
        {
            var setorCongregacao = UnidadeTrabalho.CongregacaoRepositorio.ObterPorSetorEId(setor, congregacao);
            var chamadas = UnidadeTrabalho.ChamadaRepositorio.TodosPorSetorCongregacaoAnoMes(setor, congregacao, ano, mes);
            var semanas = ObterSemanasMes(ano, mes);

            var relatorioDTO = new RelatorioDTO
            {
                Setor = setorCongregacao.SetorNumero,
                Congregacao = setorCongregacao.Id,
                NomeCongregacao = setorCongregacao.Nome,
                OfertaMes = chamadas.Sum(x => x.Oferta),
                OfertaDepartamento = chamadas.Sum(x => x.Oferta) * 0.2m,
                Semanas = new List<RelatorioSemanaDTO>()
            };

            var matriculados = UnidadeTrabalho.MatriculaRepositorio.Todos(x => x.Classe.CongregacaoId == congregacao).ToList();
            var classes = UnidadeTrabalho.ClasseRepositorio.TodosPorCongregacao(congregacao).ToList();

            foreach (var semana in semanas)
            {
                var semanaDTO = new RelatorioSemanaDTO
                {
                    InicioSemana = semana.Item1,
                    FimSemana = semana.Item2,
                    Matriculados = matriculados.Where(x => classes.Select(c => c.Id).Contains(x.ClasseId) && x.DataMatricula <= semana.Item1 && (x.DataTerminoMatricula == null || x.DataTerminoMatricula >= semana.Item2)).Count()
                };

                var chamadasSemana = chamadas.Where(x => x.Data >= semana.Item1 && x.Data <= semana.Item2);
                if (chamadasSemana.Any())
                {
                    semanaDTO.Presentes = chamadasSemana.Sum(x => x.Presencas.Count);
                    semanaDTO.Visitantes = chamadasSemana.Sum(x => x.Visitantes);
                    semanaDTO.Oferta = chamadasSemana.Sum(x => x.Oferta);
                }

                semanaDTO.Ausentes = semanaDTO.Matriculados - semanaDTO.Presentes;

                relatorioDTO.Semanas.Add(semanaDTO);
            }

            return Ok(relatorioDTO);
        }

        private IEnumerable<(DateTime, DateTime)> ObterSemanasMes(int ano, int mes)
        {
            var retorno = new List<(DateTime, DateTime)>();
            var ultimoDiaMes = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));

            DateTime comecoSemana;
            var fimSemana = new DateTime(ano, mes, 1).AddDays(-1);

            while (fimSemana < ultimoDiaMes)
            {
                comecoSemana = fimSemana.AddDays(1);
                fimSemana = comecoSemana.AddDays(DayOfWeek.Saturday - comecoSemana.DayOfWeek);

                if (fimSemana > ultimoDiaMes)
                    fimSemana = fimSemana.AddDays(-fimSemana.Subtract(ultimoDiaMes).Days);

                retorno.Add((comecoSemana, fimSemana));
            }

            return retorno.OrderBy(x => x.Item1);
        }
    }
}