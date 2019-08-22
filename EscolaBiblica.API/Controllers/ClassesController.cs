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
    [Route("api/Setores/{setor}/Congregacoes/{congregacao}/[controller]")]
    public class ClassesController : BaseController<Classe>
    {
        public ClassesController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [HttpGet]
        public IActionResult Get(int congregacao)
        {
            return Ok(UnidadeTrabalho.ClasseRepositorio.TodosPorCongregacao(congregacao));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int congregacao, int id)
        {
            return TratarRetorno(UnidadeTrabalho.ClasseRepositorio.ObterPorCongregacaoEId(congregacao, id));
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpPost]
        public IActionResult Post(int congregacao, [FromBody] Classe classe)
        {
            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var novaClasse = new Classe
                {
                    Nome = classe.Nome,
                    CongregacaoId = congregacao
                };

                if (!string.IsNullOrWhiteSpace(classe.Descricao))
                    novaClasse.Descricao = classe.Descricao;

                unidadeTrabalho.ClasseRepositorio.Adicionar(novaClasse);
            });
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpPut("{id}")]
        public IActionResult Put(int congregacao, int id, [FromBody] Classe classe)
        {
            var classeAlterar = UnidadeTrabalho.ClasseRepositorio.ObterPorCongregacaoEId(congregacao, id);
            if (classeAlterar == null)
                return NotFound();

            if (classeAlterar.Nome == classe.Nome && classeAlterar.Descricao == classe.Descricao)
                return Ok();

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                if (classeAlterar.Nome != classe.Nome)
                    classeAlterar.Nome = classe.Nome;

                if (!string.IsNullOrWhiteSpace(classe.Descricao) && classeAlterar.Descricao != classe.Descricao)
                    classeAlterar.Descricao = classe.Descricao;

                unidadeTrabalho.ClasseRepositorio.Alterar(classeAlterar);
            });
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return TratarRetornoTransacao(unidadeTrabalho => unidadeTrabalho.ClasseRepositorio.Excluir(id));
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
        [HttpGet("{id}/Alunos")]
        public IActionResult ObterAlunosMatriculados(int id)
        {
            var matriculas = UnidadeTrabalho.MatriculaRepositorio.Todos(x => x.ClasseId == id && x.DataTerminoMatricula == null, include: x => x.Aluno);

            return Ok(matriculas.Select(x => x.Aluno));
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
        [HttpPost("{id}/Alunos")]
        public IActionResult MatricularAlunos(int id, [FromBody] IEnumerable<MatriculaDTO> alunos)
        {
            var dateNow = DateTime.Now;

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var idsAlunos = alunos.Select(x => x.AlunoId);
                var matriculasAbertas = unidadeTrabalho.MatriculaRepositorio.Todos(x => x.ClasseId != id && x.DataTerminoMatricula == null && idsAlunos.Contains(x.AlunoId));
                if (matriculasAbertas.Any())
                {
                    foreach (var matricula in matriculasAbertas)
                        matricula.DataTerminoMatricula = dateNow;

                    unidadeTrabalho.MatriculaRepositorio.Alterar(matriculasAbertas);
                }

                var matriculasNovas = alunos.Select(x => new Matricula
                {
                    ClasseId = id,
                    AlunoId = x.AlunoId,
                    DataMatricula = dateNow
                });

                unidadeTrabalho.MatriculaRepositorio.Adicionar(matriculasNovas);
            });
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
        [HttpDelete("{id}/Alunos")]
        public IActionResult DesmatricularAlunos(int id, [FromBody] IEnumerable<MatriculaDTO> alunos)
        {
            var dateNow = DateTime.Now;

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var idsAlunos = alunos.Select(x => x.AlunoId);
                var matriculas = unidadeTrabalho.MatriculaRepositorio.Todos(x => x.ClasseId == id && idsAlunos.Contains(x.AlunoId) && x.DataTerminoMatricula == null);

                foreach (var matricula in matriculas)
                    matricula.DataTerminoMatricula = dateNow;

                unidadeTrabalho.MatriculaRepositorio.Alterar(matriculas);
            });
        }
    }
}