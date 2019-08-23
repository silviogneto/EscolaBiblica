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
                if (chamada.Biblias != chamadaDto.Biblias)
                    chamada.Biblias = chamadaDto.Biblias;

                if (chamada.Revistas != chamadaDto.Revistas)
                    chamada.Revistas = chamadaDto.Revistas;

                if (chamada.Visitantes != chamadaDto.Visitantes)
                    chamada.Visitantes = chamadaDto.Visitantes;

                if (chamada.Oferta != chamadaDto.Oferta)
                    chamada.Oferta = chamadaDto.Oferta;

                if (chamadaDto.Presencas != null)
                    chamada.Presencas = new List<Presenca>(chamadaDto.Presencas.Select(x => new Presenca
                    {
                        IdAluno = x.IdAluno,
                        NomeAluno = x.NomeAluno
                    }));

                unidadeTrabalho.ChamadaRepositorio.Alterar(chamada);
            });
        }
    }
}