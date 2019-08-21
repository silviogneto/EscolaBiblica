using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class AuthenticationRepositorio
    {
        public async Task<UsuarioDTO> Authenticate(string usuario, string senha)
        {
            var config = new WebRequestConfig
            {
                Servidor = "http://192.168.1.106:5000/api",
                EndPoint = "authentication"
            };

            try
            {
                return await new WebRequestHelper(config).Post<UsuarioDTO>(new UsuarioDTO
                {
                    Login = usuario,
                    Senha = senha
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
