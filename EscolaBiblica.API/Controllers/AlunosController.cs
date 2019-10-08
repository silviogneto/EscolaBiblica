using System;
using System.Linq;
using EscolaBiblica.API.DAL;
using EscolaBiblica.API.DAL.Modelos;
using EscolaBiblica.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaBiblica.API.Controllers
{
    [Route("api/Setores/{setor}/Congregacoes/{congregacao}/[controller]")]
    public class AlunosController : BaseController<Aluno>
    {
        public AlunosController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [HttpGet]
        public IActionResult Get(int congregacao)
        {
            return Ok(UnidadeTrabalho.AlunoRepositorio.TodosPorCongregacao(congregacao));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int congregacao, int id)
        {
            return TratarRetorno(UnidadeTrabalho.AlunoRepositorio.ObterPorCongregacaoEId(congregacao, id));
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador + "," + Perfil.Professor)]
        [HttpPost]
        public IActionResult Post(int congregacao, [FromBody] Aluno aluno)
        {
            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var novoAluno = new Aluno
                {
                    Nome = aluno.Nome,
                    DataNascimento = aluno.DataNascimento,
                    CongregacaoId = congregacao
                };

                if (!string.IsNullOrWhiteSpace(aluno.Telefone))
                    novoAluno.Telefone = aluno.Telefone;

                if (aluno.Enderecos != null)
                    novoAluno.Enderecos = aluno.Enderecos;

                unidadeTrabalho.AlunoRepositorio.Adicionar(novoAluno);
            });
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador + "," + Perfil.Professor)]
        [HttpPut("{id}")]
        public IActionResult Put(int congregacao, int id, [FromBody] Aluno aluno)
        {
            var alunoAlterar = UnidadeTrabalho.AlunoRepositorio.ObterPorCongregacaoEId(congregacao, id);
            if (alunoAlterar == null)
                return NotFound();

            if (alunoAlterar.Nome == aluno.Nome &&
                alunoAlterar.DataNascimento == aluno.DataNascimento &&
                alunoAlterar.Telefone == aluno.Telefone)
                return Ok();

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                if (alunoAlterar.Nome != aluno.Nome)
                    alunoAlterar.Nome = aluno.Nome;

                if (alunoAlterar.DataNascimento != aluno.DataNascimento)
                    alunoAlterar.DataNascimento = aluno.DataNascimento;

                if (!string.IsNullOrWhiteSpace(aluno.Telefone) && alunoAlterar.Telefone != aluno.Telefone)
                    alunoAlterar.Telefone = aluno.Telefone;

                unidadeTrabalho.AlunoRepositorio.Alterar(alunoAlterar);
            });
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador + "," + Perfil.Professor)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return TratarRetornoTransacao(unidadeTrabalho => unidadeTrabalho.AlunoRepositorio.Excluir(id));
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador + "," + Perfil.Professor)]
        [HttpPost("{id}/Classes/{classeId}")]
        public IActionResult MatricuarAluno(int id, int classeId)
        {
            var dateNow = DateTime.Now;
            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var matriculasAbertas = unidadeTrabalho.MatriculaRepositorio.Todos(x => x.ClasseId != classeId && x.DataTerminoMatricula == null && x.AlunoId == id);
                if (matriculasAbertas.Any())
                {
                    foreach (var matricula in matriculasAbertas)
                        matricula.DataTerminoMatricula = dateNow;

                    unidadeTrabalho.MatriculaRepositorio.Alterar(matriculasAbertas);
                }

                unidadeTrabalho.MatriculaRepositorio.Adicionar(new Matricula
                {
                    ClasseId = classeId,
                    AlunoId = id,
                    DataMatricula = dateNow
                });
            });
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador + "," + Perfil.Professor)]
        [HttpGet("Aniversariantes/Mes")]
        public IActionResult AniversariantesMes(int congregacao)
        {
            return AniversariantesMes(congregacao, DateTime.Now.Month);
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador + "," + Perfil.Professor)]
        [HttpGet("Aniversariantes/Mes/{mes}")]
        public IActionResult AniversariantesMes(int congregacao, int mes)
        {
            return Ok(UnidadeTrabalho.AlunoRepositorio.TodosPorCongregacao(congregacao).Where(x => x.DataNascimento.Month == mes));
        }
    }
}
