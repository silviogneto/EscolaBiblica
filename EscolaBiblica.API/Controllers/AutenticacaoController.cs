using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EscolaBiblica.API.Configuracoes;
using EscolaBiblica.API.DAL;
using EscolaBiblica.API.DAL.Modelos;
using EscolaBiblica.API.DTO;
using EscolaBiblica.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EscolaBiblica.API.Controllers
{
    public class AutenticacaoController : BaseController<UsuarioDTO>
    {
        private readonly ConfiguracoesApp _configuracoesApp;

        public AutenticacaoController(
            IUnidadeTrabalho unidadeTrabalho,
            IOptions<ConfiguracoesApp> options) : base(unidadeTrabalho)
        {
            _configuracoesApp = options.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Autenticar([FromBody] UsuarioDTO dto)
        {
            var usuario = UnidadeTrabalho.UsuarioRepositorio.ObterPorLoginSenha(dto.Login, Hash.GerarHash(dto.Senha ?? ""));
            if (usuario == null)
                return Unauthorized();

            var chave = Encoding.ASCII.GetBytes(_configuracoesApp.ChaveSecreta);
            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Perfil)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resultado = new AutenticacaoDTO
            {
                Id = usuario.Id,
                Login = dto.Login,
                Nome = usuario.Nome,
                Token = tokenHandler.WriteToken(token),
                Expires = expires,
                Perfil = usuario.Perfil
            };

            IEnumerable<Classe> classes;

            switch (usuario.Perfil)
            {
                case Perfil.Admin:
                    classes = UnidadeTrabalho.ClasseRepositorio.Todos(include: x => x.Congregacao);
                    break;
                case Perfil.Coordenador:
                    var coordenador = UnidadeTrabalho.CoordenadorRepositorio.ObterPorUsuario(usuario.Id);
                    if (coordenador == null)
                        return NotFound();

                    var congregacoes = UnidadeTrabalho.CoordenadorCongregacaoRepositorio.ObterCongregacoesPorCoordenadorId(coordenador.Id).ToList();
                    if (!congregacoes.Any())
                        return BadRequest(); // TODO: Criar objeto de erro para retornar

                    classes = UnidadeTrabalho.ClasseRepositorio.TodosPorCongregacoes(congregacoes);
                    break;
                case Perfil.Professor:
                    var professor = UnidadeTrabalho.ProfessorRepositorio.ObterPorUsuario(usuario.Id);
                    if (professor == null)
                        return NotFound();

                    classes = UnidadeTrabalho.ProfessorClasseRepositorio.ObterClassesPorProfessorId(professor.Id);
                    break;
                default:
                    return BadRequest(); // TODO: Criar objeto de erro para retornar
            }

            resultado.Classes = ModeloParaDTO(classes);

            return Ok(resultado);
        }

        private IEnumerable<ClasseDTO> ModeloParaDTO(IEnumerable<Classe> classes)
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
    }
}