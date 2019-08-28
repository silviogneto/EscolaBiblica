using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EscolaBiblica.API.Configuracoes;
using EscolaBiblica.API.DAL;
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
                Token = tokenHandler.WriteToken(token),
                Expires = expires,
                Perfil = usuario.Perfil
            };

            if (usuario.Perfil == Perfil.Professor)
            {
                var professor = UnidadeTrabalho.ProfessorRepositorio.ObterPorUsuario(usuario.Id);
                if (professor != null)
                {
                    resultado.Setores = new List<SetorDTO>
                    {
                        new SetorDTO
                        {
                            Numero = professor.Congregacao.Setor.Numero,
                            Nome = professor.Congregacao.Setor.Nome,
                            Congregacoes = new List<CongregacaoDTO>
                            {
                                new CongregacaoDTO
                                {
                                    Id = professor.Congregacao.Id,
                                    Nome = professor.Congregacao.Nome
                                }
                            }
                        }
                    };
                }
            }

            return Ok(resultado);
        }
    }
}