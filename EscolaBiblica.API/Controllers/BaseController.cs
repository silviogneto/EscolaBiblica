using System;
using EscolaBiblica.API.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaBiblica.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public abstract class BaseController<TModelo> : ControllerBase
    {
        public IUnidadeTrabalho UnidadeTrabalho { get; set; }

        public BaseController(IUnidadeTrabalho unitdadeTrabalho)
        {
            UnidadeTrabalho = unitdadeTrabalho;
        }

        public IActionResult TratarRetorno(TModelo entidade)
        {
            if (entidade == null)
                return NotFound();

            return Ok(entidade);
        }

        public IActionResult TratarRetornoTransacao(Action<IUnidadeTrabalho> action)
        {
            try
            {
                UnidadeTrabalho.ExecutarTransacao(action);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}