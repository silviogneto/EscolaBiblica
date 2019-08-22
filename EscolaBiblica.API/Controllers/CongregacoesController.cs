using EscolaBiblica.API.DAL;
using EscolaBiblica.API.DAL.Modelos;
using EscolaBiblica.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaBiblica.API.Controllers
{
    [Route("api/Setores/{setor}/[controller]")]
    public class CongregacoesController : BaseController<Congregacao>
    {
        public CongregacoesController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [HttpGet]
        public IActionResult Get(int setor)
        {
            return Ok(UnidadeTrabalho.CongregacaoRepositorio.TodosPorSetor(setor));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int setor, int id)
        {
            return TratarRetorno(UnidadeTrabalho.CongregacaoRepositorio.ObterPorSetorEId(setor, id));
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpPost]
        public IActionResult Post(int setor, [FromBody] Congregacao congregacao)
        {
            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var novaCongregacao = new Congregacao
                {
                    Nome = congregacao.Nome,
                    SetorNumero = setor
                };

                unidadeTrabalho.CongregacaoRepositorio.Adicionar(novaCongregacao);
            });
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpPut("{id}")]
        public IActionResult Put(int setor, int id, [FromBody] Congregacao congregacao)
        {
            var congregacaoAlterar = UnidadeTrabalho.CongregacaoRepositorio.ObterPorSetorEId(setor, id);
            if (congregacaoAlterar == null)
                return NotFound();

            if (congregacaoAlterar.Nome == congregacao.Nome)
                return Ok();

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                congregacaoAlterar.Nome = congregacao.Nome;

                unidadeTrabalho.CongregacaoRepositorio.Alterar(congregacaoAlterar);
            });
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return TratarRetornoTransacao(unidadeTrabalho => unidadeTrabalho.CongregacaoRepositorio.Excluir(id));
        }
    }
}