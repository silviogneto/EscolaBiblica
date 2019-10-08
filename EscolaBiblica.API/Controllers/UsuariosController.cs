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
    public class UsuariosController : BaseController<Usuario>
    {
        public UsuariosController(IUnidadeTrabalho unitdadeTrabalho) : base(unitdadeTrabalho)
        {
        }

        [HttpGet("{id}/Classes")]
        public IActionResult ObterClassesPorUsuario(int id)
        {
            var usuario = UnidadeTrabalho.UsuarioRepositorio.ObterPorId(id);
            if (usuario == null)
                return NotFound();

            switch (usuario.Perfil)
            {
                case Perfil.Admin:
                    return Ok(ClasseParaDTO(UnidadeTrabalho.ClasseRepositorio.Todos(include: x => x.Congregacao)));
                case Perfil.Coordenador:
                    var coordenador = UnidadeTrabalho.CoordenadorRepositorio.ObterPorUsuario(usuario.Id);
                    if (coordenador == null)
                        return NotFound();

                    var congregacoes = UnidadeTrabalho.CoordenadorCongregacaoRepositorio.ObterCongregacoesPorCoordenadorId(coordenador.Id).ToList();
                    if (!congregacoes.Any())
                        return BadRequest(); // TODO: Criar objeto de erro para retornar

                    return Ok(ClasseParaDTO(UnidadeTrabalho.ClasseRepositorio.TodosPorCongregacoes(congregacoes)));
                case Perfil.Professor:
                    var professor = UnidadeTrabalho.ProfessorRepositorio.ObterPorUsuario(usuario.Id);
                    if (professor == null)
                        return NotFound();

                    return Ok(ClasseParaDTO(UnidadeTrabalho.ProfessorClasseRepositorio.ObterClassesPorProfessorId(professor.Id)));
            }

            return BadRequest(); // TODO: Criar objeto de erro para retornar
        }

        [Authorize(Roles = Perfil.Admin + "," + Perfil.Coordenador)]
        [HttpGet("{id}/Congregacoes")]
        public IActionResult ObterSetoresCongregacoesPorUsuario(int id)
        {
            var usuario = UnidadeTrabalho.UsuarioRepositorio.ObterPorId(id);
            if (usuario == null)
                return NotFound();

            switch (usuario.Perfil)
            {
                case Perfil.Admin:
                    return Ok(CongregacoesParaDTO(UnidadeTrabalho.CongregacaoRepositorio.Todos(include: x => x.Setor)));
                case Perfil.Coordenador:
                    var coordenador = UnidadeTrabalho.CoordenadorRepositorio.ObterPorUsuario(usuario.Id);
                    if (coordenador == null)
                        return NotFound();

                    var congregacoes = UnidadeTrabalho.CoordenadorCongregacaoRepositorio.ObterCongregacoesPorCoordenadorId(coordenador.Id).ToList();
                    if (!congregacoes.Any())
                        return BadRequest();

                    return Ok(CongregacoesParaDTO(congregacoes));
            }

            return BadRequest();
        }

        private IEnumerable<ClasseDTO> ClasseParaDTO(IEnumerable<Classe> classes)
        {
            return classes.Select(x => new ClasseDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
                Congregacao = x.Congregacao.Id,
                CongregacaoNome = x.Congregacao.Nome,
                Setor = x.Congregacao.Setor.Numero,
                SetorNome = x.Congregacao.Setor.Nome
            });
        }

        private IEnumerable<SetorDTO> CongregacoesParaDTO(IEnumerable<Congregacao> congregacoes)
        {
            return congregacoes.GroupBy(x => x.Setor).Select(x => new SetorDTO
            {
                Numero = x.Key.Numero,
                Nome = x.Key.Nome,
                Congregacoes = x.Select(c => new CongregacaoDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
            });
        }
    }
}