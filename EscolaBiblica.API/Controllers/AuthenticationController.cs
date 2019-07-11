using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EscolaBiblica.API.DTO;
using EscolaBiblica.API.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EscolaBiblica.API.Controllers
{
    public class AuthenticationController : BaseController<UsuarioDTO>
    {
        private readonly AppSettings _appSettings;

        public AuthenticationController(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UsuarioDTO dto)
        {
            var usuario = new { Id = 0, Perfil = "" };
            if (usuario == null)
                return Unauthorized();

            var chave = Encoding.ASCII.GetBytes(_appSettings.ChaveSecreta);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Perfil)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new UsuarioDTO
            {
                Id = usuario.Id,
                Login = dto.Login,
                Token = tokenHandler.WriteToken(token),
                Perfil = usuario.Perfil
            });
        }
    }
}