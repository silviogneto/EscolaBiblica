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

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
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

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
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

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Professor)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return TratarRetornoTransacao(unidadeTrabalho => unidadeTrabalho.AlunoRepositorio.Excluir(id));
        }
    }
}
