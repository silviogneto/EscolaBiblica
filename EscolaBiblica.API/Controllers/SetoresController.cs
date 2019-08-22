using EscolaBiblica.API.DAL;
using EscolaBiblica.API.DAL.Modelos;
using EscolaBiblica.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaBiblica.API.Controllers
{
    public class SetoresController : BaseController<Setor>
    {
        public SetoresController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(UnidadeTrabalho.SetorRepositorio.Todos());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return TratarRetorno(UnidadeTrabalho.SetorRepositorio.ObterPorId(id));
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] Setor setor)
        {
            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                var novoSetor = new Setor
                {
                    Numero = setor.Numero,
                    Nome = setor.Nome
                };

                unidadeTrabalho.SetorRepositorio.Adicionar(novoSetor);
            });
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Setor setor)
        {
            var setorAlterar = UnidadeTrabalho.SetorRepositorio.ObterPorId(id);
            if (setorAlterar == null)
                return NotFound();

            if (setorAlterar.Nome == setor.Nome)
                return Ok();

            return TratarRetornoTransacao(unidadeTrabalho =>
            {
                setorAlterar.Nome = setor.Nome;

                unidadeTrabalho.SetorRepositorio.Alterar(setorAlterar);
            });
        }

        [Authorize(Roles = Perfil.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return TratarRetornoTransacao(unidadeTrabalho => unidadeTrabalho.SetorRepositorio.Excluir(id));
        }
    }
}