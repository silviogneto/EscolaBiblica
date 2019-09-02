using System;
using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.API.DAL;
using EscolaBiblica.API.DAL.Modelos;
using EscolaBiblica.API.DTO;
using EscolaBiblica.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaBiblica.API.Controllers
{
    [Route("api/Setores/{setor}/Congregacoes/{congregacao}/Classes/{classe}/[controller]")]
    public class ChamadasController : BaseController<Chamada>
    {
        public ChamadasController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
        [HttpGet("{data}")]
        public IActionResult Get(int classe, DateTime data)
        {
            var retorno = new ChamadaDTO();

            var chamada = UnidadeTrabalho.ChamadaRepositorio.ObterPorClasseEData(classe, data);
            if (chamada != null)
            {
                retorno.Biblias = chamada.Biblias;
                retorno.Revistas = chamada.Revistas;
                retorno.Visitantes = chamada.Visitantes;
                retorno.Oferta = chamada.Oferta;

                if (chamada.Presencas != null && chamada.Presencas.Any())
                    retorno.Presencas = chamada.Presencas.Select(x => new AlunoDTO { Id = x.IdAluno, Nome = x.NomeAluno });
            }

            // Busca alunos matriculados
            var matriculas = UnidadeTrabalho.MatriculaRepositorio.Todos(x => x.ClasseId == classe &&
                                                                             x.DataMatricula <= data.Date &&
                                                                             (x.DataTerminoMatricula == null || x.DataTerminoMatricula > data.Date), include: x => x.Aluno);
            if (matriculas.Any())
            {
                retorno.Matriculados = matriculas.Select(x => new AlunoDTO { Id = x.Aluno.Id, Nome = x.Aluno.Nome });
            }

            return Ok(retorno);
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
        [HttpPost("{data}")]
        public IActionResult Post(int classe, DateTime data, [FromBody] ChamadaDTO chamadaDto)
        {
            var chamada = UnidadeTrabalho.ChamadaRepositorio.ObterPorClasseEData(classe, data);
            if (chamada == null)
            {
                chamada = new Chamada { ClasseId = classe, Data = data.Date };

                UnidadeTrabalho.ChamadaRepositorio.Adicionar(chamada);
                UnidadeTrabalho.Salvar();
            }

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                if (chamadaDto.Visitantes.HasValue && chamada.Visitantes != chamadaDto.Visitantes)
                    chamada.Visitantes = chamadaDto.Visitantes.Value;

                if (chamadaDto.Biblias.HasValue && chamada.Biblias != chamadaDto.Biblias)
                    chamada.Biblias = chamadaDto.Biblias.Value;

                if (chamadaDto.Revistas.HasValue && chamada.Revistas != chamadaDto.Revistas)
                    chamada.Revistas = chamadaDto.Revistas.Value;

                if (chamadaDto.Oferta.HasValue && chamada.Oferta != chamadaDto.Oferta)
                    chamada.Oferta = chamadaDto.Oferta.Value;

                if (chamadaDto.Presencas != null)
                {
                    chamada.Presencas = new List<Presenca>(chamadaDto.Presencas.Select(x => new Presenca
                    {
                        IdAluno = x.Id,
                        NomeAluno = x.Nome
                    }));
                }

                unidadeTrabalho.ChamadaRepositorio.Alterar(chamada);
            });
        }
    }
}