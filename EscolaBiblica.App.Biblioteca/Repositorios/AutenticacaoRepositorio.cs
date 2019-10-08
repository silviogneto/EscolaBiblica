using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class AutenticacaoRepositorio
    {
        private readonly string URL = "http://192.168.1.165:5000/api"; //"http://10.0.2.2:5000/api"

        public async Task<AutenticacaoDTO> Autenticar(string usuario, string senha)
        {
            var config = new WebRequestConfig(URL)
            {
                EndPoint = "Autenticacao"
            };

            return await new WebRequestHelper(config).Post<AutenticacaoDTO>(new AutenticacaoDTO
            {
                Login = usuario,
                Senha = senha
            });
        }
    }
}
