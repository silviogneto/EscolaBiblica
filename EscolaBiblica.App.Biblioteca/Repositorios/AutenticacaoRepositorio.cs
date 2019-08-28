using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class AutenticacaoRepositorio
    {
        public async Task<UsuarioDTO> Autenticar(string usuario, string senha)
        {
            var config = new WebRequestConfig("http://10.0.2.2:5000/api")
            {
                EndPoint = "Autenticacao"
            };

            return await new WebRequestHelper(config).Post<UsuarioDTO>(new UsuarioDTO
            {
                Login = usuario,
                Senha = senha
            });
        }
    }
}
